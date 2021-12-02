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
                var cmd = new SqlCommand(sql, conn);
                if(cmdType == 2)
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
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
                var cmd = new SqlCommand(sql, conn);
                if (cmdType == 2)
                    cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                conn.Open();
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
            
            var cmd = new SqlCommand(sql, conn);
            if (cmdType == 2)
                cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
            try
            {
                conn.Open();
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
                SqlCommand cmd = new SqlCommand(sql,conn);
                if (cmdType == 2)
                    cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(ds);
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// 填充dt，一个结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
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
        /// <param name="listSql"></param>
        /// <returns></returns>
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
                    trans.Commit();
                    return true;
                }catch(SqlException ex)
                {
                    trans.Rollback();
                    throw new Exception("执行事务出现异常", ex);
                }
            }
        }
    }
}
