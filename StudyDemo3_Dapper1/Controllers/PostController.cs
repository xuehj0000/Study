using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public List<Post> Get()
        {
            var value = new PostDAL().GetPosts();
            var posts = (List<Post>)MemoryHelper.Get("post");
            return posts;
        }
        
    }
}
