using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace StudyDemo3_Dapper
{
    public class DapperHelper
    {
        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="buffered">缓冲/缓存</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public T QueryFirst<T>(string sql, object param = null, IDbTransaction trans = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            ConnOptions.DbConnection.Open();
            using (trans = ConnOptions.DbConnection.BeginTransaction())
            {
                var user = ConnOptions.DbConnection.QueryFirstOrDefault<T>(sql, param, trans, commandTimeout, commandType);
                ConnOptions.DbConnection.Close();
                return user;
            }
        }

        /// <summary>
        /// 多个查询
        /// </summary>
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction trans = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return ConnOptions.DbConnection.Query<T>(sql, param, trans, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Query<T>(string sql, object param = null, IDbTransaction trans = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return ConnOptions.DbConnection.Execute(sql, param, trans, commandTimeout, commandType);
        }

    }
}
