using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Newtonsoft.Json;

using PhoneBook.Api.Infrastructure.Models.Bases;

namespace PhoneBook.Api.Infrastructure.Models {

    public class SubmissionSucceededModel : ResponseBase {

        [DefaultValue("")]
        [JsonProperty(PropertyName = "Result")]
        public object Result { get; set; }
    }
}
