using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Phonebook.Api.Infrastructure.Extensions;
using Phonebook.Api.Infrastructure.Models;

namespace Phonebook.Api.Infrastructure.Results {

    public class SubmissionFailedResult : ObjectResult {

        public SubmissionFailedResult(string result, string message, string source, string status = "FAILED", Exception exception = null)
            : base(new SubmissionFailedModel(result, message, StatusCodes.Status500InternalServerError, status, source, exception)) {

            StatusCode = exception != null ? (int)exception.GetHttpStatusCode() : StatusCodes.Status500InternalServerError;
        }
    }
}