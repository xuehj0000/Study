using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo4_UserService.Entity
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }

        public static List<Users> GetList()
        {
            var list = new List<Users>()
            {
                new Users{Id=1, UserName="一天", Age=19},
                new Users{Id=2, UserName="二天", Age=19},
                new Users{Id=3, UserName="三天", Age=19},
                new Users{Id=4, UserName="四天", Age=19},
                new Users{Id=5, UserName="五天", Age=19},
                new Users{Id=6, UserName="六天", Age=19}
            };
            return list;
        }
    }
}
