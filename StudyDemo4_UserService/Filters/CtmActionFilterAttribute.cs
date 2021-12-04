using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDemo4_UserService
{
    /// <summary>
    /// 限制用户访问次数，
    /// </summary>
    public class CtmActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 静态单例
        /// </summary>
        private static Dictionary<string, List<DateTime>> dicRequestTimes = new Dictionary<string, List<DateTime>>(); 

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var port = context.HttpContext.Connection.RemotePort;

            var requestName = $"{ip}_{port}";

            // 查找是否包含当前地址
            var hasHost = dicRequestTimes.ContainsKey(requestName);
            if (hasHost)
            {
                var allTimeList = dicRequestTimes[requestName];
                // 访问次数为空或者小于5次则正常访问
                if(allTimeList == null || allTimeList.Count < 5)
                {
                    allTimeList.Add(DateTime.Now);
                }
                else
                {
                    // 获取当前时间到当前时间前1分钟时间段内的访问次数
                    var timeCount = allTimeList.Count(r => r > DateTime.Now.AddMinutes(-1));
                    if (timeCount > 5)
                    {
                        // 返回错误
                        context.Result = new JsonResult("访问过于频繁，请稍后再试") { StatusCode = 500 };     // 短路器
                    }
                    else
                    {
                        allTimeList.Add(DateTime.Now);
                        // 清空1分钟之前添加的记录
                        var lessCount = allTimeList.Count - timeCount;
                        allTimeList.RemoveRange(0, lessCount - 1);
                    }
                }
            }
            else
            {
                List<DateTime> value = new List<DateTime>();
                value.Add(DateTime.Now);
                dicRequestTimes.Add(requestName, value);
            }
        }
    }
}
