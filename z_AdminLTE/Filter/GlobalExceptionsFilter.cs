using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace z_AdminLTE
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter: IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionsFilter> _logger;
        public GlobalExceptionsFilter(ILogger<GlobalExceptionsFilter> logger)
        {
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            if(context.ExceptionHandled == false)
            {
                var ret = new SysResult(false, context.Exception.Message);
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status200OK,             // 返回状态码设置为200，表示成功
                    ContentType = "application/json;charset=utf-8",   // 设置返回格式
                    Content = JsonConvert.SerializeObject(ret)
                };
                var msg = ret.Msg + string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { ret.Msg, context.Exception.GetType().Name, context.Exception.Message, context.Exception.StackTrace });
                _logger.LogError(msg);
            }
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
