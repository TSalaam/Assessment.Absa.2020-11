using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Phonebook.Api.Infrastructure.Filters {

    public class ActionLogFilter : IActionFilter {

        private readonly ILogger _logger;

        public ActionLogFilter(ILoggerFactory loggerFactory) {
            _logger = loggerFactory.CreateLogger("ActionLogFilter");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <see cref="https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters#action-filters"/>
        public void OnActionExecuting(ActionExecutingContext context) {

            _logger.LogInformation("OnActionExecuting");

            var headersDictionary = context.HttpContext.Request.Headers;

            var remoteIpAddress = context.HttpContext.Connection.RemoteIpAddress;
            var localIpAddress = context.HttpContext.Connection.LocalIpAddress;
            var userAgent = headersDictionary["User-Agent"];
            string controller = context.Controller.ToString();

            var message = string.Format("userAgent: {0}| remoteIpAddress: {1}| localIpAddress: {2}| controller: {3}", userAgent, remoteIpAddress, localIpAddress, controller);

            _logger.LogTrace(message);
        }

        public void OnActionExecuted(ActionExecutedContext context) {

        }

        public void OnResultExecuting(ResultExecutingContext context) {

        }

        public void OnResultExecuted(ResultExecutedContext context) {

        }
    }
}
