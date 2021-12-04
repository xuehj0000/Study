using Microsoft.AspNetCore.Mvc;
using StudyDemo3_Dapper1;

namespace StudyDemo3_Dapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpGet("{n}/{p}")]  // 设置传参规则
        public Users Get(string n, string p)
        {
            var dal = new UserDAL();
            var model = dal.GetUserByLogin(n, p);
            return model;
        }

        //[HttpGet("{n}-{p}")]   // 设置传参规则
        //public Users GetLoginParams(string n, string p)
        //{
        //    var dal = new UserDAL();
        //    var model = dal.GetUserByLogin(n, p);
        //    return model;
        //}

        //[HttpGet]              // 普通规则
        //public Users GetLogin(string name, string password)
        //{
        //    var dal = new UserDAL();
        //    var model = dal.GetUserByLogin(name, password);
        //    return model;
        //}
}
}
