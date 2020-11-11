using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PhoneBook.Api.Infrastructure.Models;

namespace PhoneBook.Api.Infrastructure.Results {

    public class SubmissionSucceededResult : OkObjectResult {

        public SubmissionSucceededResult(object result, string message, string source, string status = "SUCCEEDED")
            : base(new SubmissionSucceededModel() { Result = result, Message = message, Source = source, Status = status }) {

            StatusCode = StatusCodes.Status200OK;
        }
    }
}
