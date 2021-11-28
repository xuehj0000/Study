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
        private readonly MyDBContext _context;

        public LoginController(MyDBContext context)
        {
            _context = context;
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
        public IActionResult Register()
        {
            return View();
        }

        #endregion

        #region 接口
        /// <summary>
        /// 登录请求
        /// </summary>
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)//模型数据验证
            {
                //if (await _context.Users.AnyAsync(a => a.Account == user.Account && a.Password == user.Password))//登陆验证
                //{
                //    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Account) };
                //    var claimnsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //    await HttpContext.SignInAsync(new ClaimsPrincipal(claimnsIdentity), new AuthenticationProperties { IsPersistent = true });
                //}
                //else
                //{
                //    return RedirectToAction(nameof(Login));
                //}
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
            return Redirect(user.ReturnUrl ?? "/");

        }


        /// <summary>
        /// 退出请求
        /// </summary>
        public async Task<IActionResult> LoginOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion



    }
}
