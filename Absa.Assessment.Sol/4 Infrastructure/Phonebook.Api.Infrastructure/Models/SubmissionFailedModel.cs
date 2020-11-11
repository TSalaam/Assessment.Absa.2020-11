using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;

using Newtonsoft.Json;

using Phonebook.Api.Infrastructure.Extensions;
using Phonebook.Api.Infrastructure.Models.Bases;

namespace Phonebook.Api.Infrastructure.Models {

    public class SubmissionFailedModel : ResponseBase {

        [DefaultValue("")]
        [JsonProperty(PropertyName = "Result")]
        public object Result { get; set; }

        [DefaultValue("")]
        [JsonProperty(PropertyName = "Details")]
        public string Details { get; set; }

        public SubmissionFailedModel(string result, string message, int statusCode, string status, string source, Exception exception = null) {

            if (exception != null && exception is System.Data.SqlClient.SqlException) {

                var e = (System.Data.SqlClient.SqlException)exception;

                int severity = e.SqlExceptionSeverity();

                // Levels of Severity: https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-error-severities?view=sql-server-ver15#levels-of-severity
                // 16 --> Indicates general errors that can be corrected by the user
                // If severity != 16, assign generic value to 'message' variable

                if (severity == 16) {
                    message = e.Message;
                }
                else {                    
                    message = Properties.Resources.ErrorMessage_SqlException_SeverityNot16;
                }
            }

            Result = result;
            Message = message;
            Details = exception != null ? exception.Message : message;
            StatusCode = exception != null ? (int)exception.GetHttpStatusCode() : statusCode;
            Status = status;
            Source = source;
        }
    }
}
