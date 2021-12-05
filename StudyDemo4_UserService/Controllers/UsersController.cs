using Microsoft.AspNetCore.Mvc;
using StudyDemo4_UserService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo4_UserService.Controllers
{
    [Route("user/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return Users.GetList();
        }

        [HttpGet("{name}")]
        public IEnumerable<Users> Get(string name)
        {
            var users = Users.GetList().Where(r => r.UserName.Contains(name));
            return users;
        }
    }
}
