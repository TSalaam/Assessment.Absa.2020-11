using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.Extensions.Logging;

using Phonebook.Api.Infrastructure.ActionResults;
using Phonebook.Api.Infrastructure.Exceptions;
using Phonebook.Api.Infrastructure.Extensions;

namespace Phonebook.Api.Infrastructure.Filters {

    public class HttpGlobalExceptionFilter : IExceptionFilter {

        private readonly IHostingEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger) {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context) {

            Exception exception = context.Exception.InnermostException();

            int statusCode = 0;

            logger.LogError(new EventId(context.Exception.HResult),
                exception,
                exception.Message);

            var json = new JsonErrorResponse();

            if (env.IsDevelopment()) {
                json.DeveloperMessage = exception.StackTrace;
            }

            var clientExceptions = new List<Type>() {
                typeof(ApiArgumentException)
            };

            if (clientExceptions.Any(exc => exc.Equals(context.Exception.GetType()))) {

                statusCode = (int)HttpStatusCode.BadRequest;

                json.StatusCode = statusCode;
                json.Status = "ERROR";
                json.Messages = new[] {
                   exception.Message
                };

                context.Result = new BadRequestObjectResult(json);

                context.HttpContext.Response.StatusCode = statusCode;
            }
            else {

                statusCode = (int)HttpStatusCode.InternalServerError;

                json.StatusCode = statusCode;
                json.Status = "An internal server error has occurred";
                json.Messages = new[] {
                   FriendlyErrorMessages.InternalError
                };

                context.Result = new InternalServerErrorObjectResult(json);

                context.HttpContext.Response.StatusCode = statusCode;
            }
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse {

            [DefaultValue(200)]
            public int StatusCode { get; set; }

            [DefaultValue("")]
            public Object Status { get; set; }

            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }

        private struct FriendlyErrorMessages {
            public static string InternalError {
                get {
                    return @"An error has occurred while processing your request. Our support team has been notified of the problem. 
If you believe you have additional information that may be of help in reproducing or correcting the error, please contact thaabiet.salaam@outlook.com. 
We apologize for the inconvenience.";
                }
            }
        }
    }
}
