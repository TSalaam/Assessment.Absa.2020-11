﻿using System.ComponentModel;

using Newtonsoft.Json;

using Phonebook.Api.Infrastructure.Models.Bases;

namespace Phonebook.Api.Infrastructure.Models {

    public class SubmissionProcessingFailedModel : ResponseBase {

        [DefaultValue("")]
        [JsonProperty(PropertyName = "Result")]
        public object Result { get; set; }

        public SubmissionProcessingFailedModel(object result, string message, string status, string source) {

            Result = result;
            Message = message;
            Status = status;
            Source = source;
        }
    }
}
