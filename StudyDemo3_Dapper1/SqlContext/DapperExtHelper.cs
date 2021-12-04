using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    public class DapperExtHelper<T> where T:BaseEntity
    {
        public T Get(int id)
        {
            return ConnOptions.DbConnection.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return ConnOptions.DbConnection.GetAll<T>();
        }
        public long Insert(T t)
        {
            return ConnOptions.DbConnection.Insert(t);
        }
        public bool Update(T t)
        {
            return ConnOptions.DbConnection.Update(t);
        }
        public bool Delete(T t)
        {
            return ConnOptions.DbConnection.Delete(t);
        }
        public bool DeleteAll(int id)
        {
            return ConnOptions.DbConnection.DeleteAll<T>();
        }

    }
}
