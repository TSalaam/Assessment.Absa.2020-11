using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PhoneBook.Api.Infrastructure.Extensions;
using PhoneBook.Api.Infrastructure.Models;

namespace PhoneBook.Api.Infrastructure.Results {

    public class SubmissionFailedResult : ObjectResult {

        public SubmissionFailedResult(string result, string message, string source, string status = "FAILED", Exception exception = null)
            : base(new SubmissionFailedModel(result, message, StatusCodes.Status500InternalServerError, status, source, exception)) {

            StatusCode = exception != null ? (int)exception.GetHttpStatusCode() : StatusCodes.Status500InternalServerError;
        }
    }
}