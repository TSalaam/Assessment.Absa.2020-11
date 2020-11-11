using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace PhoneBook.Api.Infrastructure.Models {

    public class ValidationError {

        [JsonProperty(PropertyName = "Field", NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        [JsonProperty(PropertyName = "ErrorMessage")]
        public string ErrorMessage { get; }

        public ValidationError(string field, string errorMessage) {
            Field = field != string.Empty ? field : null;
            ErrorMessage = errorMessage;
        }
    }
}
