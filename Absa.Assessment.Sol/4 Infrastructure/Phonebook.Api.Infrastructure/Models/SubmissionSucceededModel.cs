using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Newtonsoft.Json;

using Phonebook.Api.Infrastructure.Models.Bases;

namespace Phonebook.Api.Infrastructure.Models {

    public class SubmissionSucceededModel : ResponseBase {

        [DefaultValue("")]
        [JsonProperty(PropertyName = "Result")]
        public object Result { get; set; }
    }
}
