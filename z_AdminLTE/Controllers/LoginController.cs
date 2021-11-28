using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace z_AdminLTE.Controllers
{
    /// <summary>
    /// 可以不加，为了记录怎么忽略验证
    /// </summary>
    [Authorize]  
    public class LoginController : Controller
    {
        private readonly MyDbBase _services;
        public LoginController(IDbFactory factory)
        {
            _services = factory.CreateClient("SqlDb");
        }



        #region 加载页面

        /// <summary>
        /// 忽略，不验证
        /// 显示登录页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 加载注册页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        #endregion

        #region 接口
        /// <summary>
        /// 登录请求
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                if (ModelState.IsValid)//模型数据验证
                {
                    var sql = $"select * from [dbo].[User] where Account=N'{user.Account}' and Password='{user.Password}'";

                    var model = _services.QueryFirst<User>(sql);
                    if (model != null)
                    {
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Account) };
                        var claimnsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(new ClaimsPrincipal(claimnsIdentity), new AuthenticationProperties { IsPersistent = true });
                    }
                    else
                    {
                        RedirectToAction(nameof(Login));
                    }
                }
                else
                {
                    UnprocessableEntity(ModelState);
                }
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                Console.WriteLine("", ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 退出请求
        /// </summary>
        public async Task<IActionResult> LoginOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion



    }
}
