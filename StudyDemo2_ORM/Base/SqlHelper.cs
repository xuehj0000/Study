using System;
using System.Data.SqlClient;
using System.Linq;

namespace StudyDemo2_ORM
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlHelper
    {
        private static string ConnectionStringCustomer = ConfigurationManager.SqlConnectionStringCustom;

        /// <summary>
        /// 通用查询，一个方法满足多种类型的查询
        /// </summary>
        public T Find<T>(int id) where T : BaseEntity,new()
        {
            Type type = typeof(T);
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.FindOne);
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Id", id) };
            
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomer))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = new T(); 
                    foreach(var prop in type.GetProperties())
                    {
                        var propName = prop.GetMappingName();
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);    // 数据库为null时，查询出来的是DbNull类型，不能直接赋值给Nullable，如下处理
                    }
                    return t;
                }
                else
                {
                    return null;
                }
            }



        }

        /// <summary>
        /// 通用插入，动态生成sql时，忽略掉添加忽略特性的属性
        /// </summary>
        public bool Insert<T>(T t) where T : BaseEntity,new()
        {
            Type type = typeof(T);
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.Insert);
            var parameters = type.GetProperties().Select(r => new SqlParameter($"@{r.GetMappingName()}", r.GetValue(t)??DBNull.Value)).ToArray();
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomer))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                int ret = command.ExecuteNonQuery();
                return ret == 1;
            }
        }

        /// <summary>
        /// 通用更新
        /// </summary>
        public bool Update<T>(T t) where T : BaseEntity, new()
        {
            if (!t.Validate())
            {
                throw new Exception("数据校验失败！");
            }

            Type type = typeof(T);
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.Update);
            var parameters = type.GetPropsWithOutIgnore().Select(r => new SqlParameter($"@{r.GetMappingName()}", r.GetValue(t)??DBNull.Value)).Append(new SqlParameter("@Id", t.Id)).ToArray();

            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomer))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                return command.ExecuteNonQuery() ==1;
            }

        }

    }

    #region

    // 问题--特殊字符--sql注入， ‘，号导致异常’， sql 注入：');select * from user where 1=1;--
    // 解决sql注入：1.数据清洗，转码；2.参数化；3.使用 orm 框架(就是参数化)

    // 数据库有规则，不应该留到数据库校验。还有的时候有业务规则（state只能是012），数据入库前一定要检验，不能相信客户端检测
    // 一站式通用数据校验，集中校验，规则各不相同，只能一一提供，额外提供一点格式说明
    #endregion


}
