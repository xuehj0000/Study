using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo3_Dapper.Controllers
{
    [EnableCors("any")]
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public List<Posts> Get()
        {
            var value = new PostDAL().GetPosts();
            var posts = (List<Posts>)MemoryHelper.Get("post");
            return posts;
        }
        
    }
}
