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
        public T Find<T>(int id) where T:BaseEntity,new()
        {
            // ①基于泛型完成类型通用，基于泛型约束保证类型正确
            Type type = typeof(T);
            // ②基于反射完成sql动态拼装，不同类型产生不同sql
            var columnString = string.Join(",", type.GetProperties().Select(r => $"[{r.GetMappingName()}]"));
            var sql = $@"select {columnString} from [{type.GetMappingName()}] where id={id}";                      // 基于特性，解决类与表映射名称不一致问题

            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomer))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // ③通过反射完成数据的动态绑定，赋值
                    //T t = Activator.CreateInstance<T>();         // 根据类型构建默认实例
                    T t = new T();  // 加new() 约束后，就可以这样
                    foreach(var prop in type.GetProperties())
                    {
                        var propName = prop.GetMappingName();
                        // 数据库为null时，查询出来的是DbNull类型，不能直接赋值给Nullable，如下处理
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);
                    }
                    return t;
                }
                else
                {
                    return null;
                }
            }



        }



    }
}
