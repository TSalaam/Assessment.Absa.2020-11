
using Microsoft.AspNetCore.Mvc;

using PhoneBook.Api.Infrastructure.Models;

namespace PhoneBook.Api.Infrastructure.Results {

    public class SubmissionProcessingFailedResult : ObjectResult {

        public SubmissionProcessingFailedResult(object result, string message, string source, string status = "FAILED_NORECORDSPROCESSED")

            : base(new SubmissionProcessingFailedModel(result, message, status, source)) {

            //StatusCode = exception != null ? (int)exception.GetHttpStatusCode() : StatusCodes.Status200OK;
        }
    }
}
