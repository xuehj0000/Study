using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace StudyDemo1
{
    public class DBHelper
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 执行sql，返回受影响的行数，针对insert update delete
        /// </summary>
        public static int ExecuteNonQuery(string sql, int cmdType, params SqlParameter[] parameters)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                var cmd = BuildCommand(conn, sql, cmdType, null, parameters);
                count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            return count;
        }

        /// <summary>
        /// 执行查询，返回结果第一行第一列的值，忽略其他行或列object
        /// </summary>
        public static object ExecuteScalar(string sql, int cmdType, params SqlParameter[] parameters)
        {
            object o = null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                var cmd = BuildCommand(conn, sql, cmdType, null, parameters);
                o = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
            }
            return o;
        }

        /// <summary>
        /// 执行查询，生成SqlDataReader
        /// </summary>
        public static SqlDataReader ExecuteReader(string sql, int cmdType, params SqlParameter[] parameters)
        {
            SqlDataReader dr = null;
            SqlConnection conn = new SqlConnection(ConnStr);

            var cmd = BuildCommand(conn, sql, cmdType, null, parameters);
            try
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }
            return dr;
        }

        /// <summary>
        /// 填充DataSet，一个或多个结果集
        /// </summary>
        public static DataSet GetDataSet(string sql, int cmdType, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                var cmd = BuildCommand(conn, sql, cmdType, null, parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// 填充dt，一个结果集
        /// </summary>
        public static DataTable GetDataTable(string sql, int cmdType, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmdType == 2)
                    cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// 事务操作  针对：增删改
        /// </summary>
        public static bool ExecuteTrans(List<string> listSql)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    for(int i = 0; i < listSql.Count; i++)
                    {
                        cmd.CommandText = listSql[i];
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();    // 提交
                    return true;
                }catch(SqlException ex)
                {
                    trans.Rollback();  // 回滚
                    throw new Exception("执行事务出现异常", ex);
                }
            }
        }

        public static bool ExecuteTrans(List<CmdInfo> listCmd)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    for (int i = 0; i < listCmd.Count; i++)
                    {
                        cmd.CommandText = listCmd[i].CommandText;
                        if (listCmd[i].CmdType == 2)
                            cmd.CommandType = CommandType.StoredProcedure;
                        if(listCmd[i].Parameters != null && listCmd[i].Parameters.Length>0)
                        cmd.Parameters.AddRange(listCmd[i].Parameters);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();    // 提交
                    return true;
                }
                catch (SqlException ex)
                {
                    trans.Rollback();  // 回滚
                    throw new Exception("执行事务出现异常", ex);
                }
                finally
                {
                    trans.Dispose();    // 立即清除，时间上的延迟
                    cmd.Dispose();
                    conn.Dispose();
                }
            }
        }


        /// <summary>
        /// 通用构造cmd
        /// </summary>
        public static SqlCommand BuildCommand(SqlConnection conn, string sql, int cmdType, SqlTransaction trans, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (cmdType == 2)
                cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            if (trans != null)
                cmd.Transaction = trans;
            return cmd;
        }
    }



    /// <summary>
    /// 封装事务中每个操作类：包括命令名称，类型，参数
    /// </summary>
    public class CmdInfo
    {
        public string CommandText;          // sql或存储过程名
        public SqlParameter[] Parameters;   // 参数列表
        public int CmdType;                 // 是存储过程还是T-SQL语句

        public CmdInfo() { }

        public CmdInfo(string cmdText, int cmdType)
        {
            CommandText = cmdText;
            CmdType = cmdType;
        }

        public CmdInfo(string cmdText, int cmdType, SqlParameter[] parameters)
        {
            CommandText = cmdText;
            CmdType = cmdType;
            Parameters = parameters;
        }

    }
}
