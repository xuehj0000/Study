using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace z_AdminLTE.Filter
{
    public class MyActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var hasPermission = true;
            //权限拦截
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var identity = context.HttpContext.User as ClaimsPrincipal;
                var accountId = identity.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var accountName = identity.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                if (accountName != "admin")
                {
                         //var menuDatalist = _menuMudoleStore.GetSysmodules(Convert.ToInt32(accountId), accountName, openRedis: true);
                         //var currentUrl = context.HttpContext.Request.Path.ToString().ToLower();
                         //if (currentUrl != "/Account/AccessDenied".ToLower())
                         //{
                         //    if (menuDatalist == null && menuDatalist.Count <= 0)
                         //    {
                         //        hasPermission = false;
                         //    }
                         //    else
                         //    {
                         //        var mtypeid = (int)SysModuleType.module;
                         //        var pageList = menuDatalist.Where(x => x.moduletypeid != mtypeid).ToList();
                         //        if (!pageList.Any(x => x.url.ToLower() == currentUrl))
                         //        {
                         //            hasPermission = false;
                         //        }
                         //    }
                         //}
                }
            }
            if (!hasPermission)
            {
                //if (context.HttpContext.Request.IsAjax())
                //{
                //    var ret = new SysResult { Suscess = false, Msg = "您无权限访问", ResultData = "" };
                //    context.Result = new JsonResult(ret);
                //}
                //else
                //{
                //    context.HttpContext.Response.Redirect("/Account/AccessDenied");
                //}
            }               
                
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
