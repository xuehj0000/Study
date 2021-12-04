using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    public class UserDAL
    {
        DapperHelper _db = new DapperHelper();

        public Users GetUserByLogin(string userName, string password)
        {
            var sql = "select * from users where userName=@userName and Password=@password";
            var user = _db.QueryFirst<Users>(sql, new { userName, password });
            if (user == null)
                return default;
            else
                return user;
        }
    }
}
