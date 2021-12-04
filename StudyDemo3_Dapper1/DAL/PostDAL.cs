using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    public class PostDAL
    {
        DapperExtHelper<Posts> dapperExtHelper = new DapperExtHelper<Posts>();

        public List<Posts> GetPosts()
        {
            return dapperExtHelper.GetAll().ToList();
        }

        public Posts GetPost(int id)
        {
            return dapperExtHelper.Get(id);
        }

        public long Insert(Posts post)
        {
            return dapperExtHelper.Insert(post);
        }
    }
}
