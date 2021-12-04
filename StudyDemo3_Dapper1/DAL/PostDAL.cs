using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper
{
    public class PostDAL
    {
        DapperExtHelper<Post> dapperExtHelper = new DapperExtHelper<Post>();

        public List<Post> GetPosts()
        {
            return dapperExtHelper.GetAll().ToList();
        }

        public Post GetPost(int id)
        {
            return dapperExtHelper.Get(id);
        }

        public long Insert(Post post)
        {
            return dapperExtHelper.Insert(post);
        }
    }
}
