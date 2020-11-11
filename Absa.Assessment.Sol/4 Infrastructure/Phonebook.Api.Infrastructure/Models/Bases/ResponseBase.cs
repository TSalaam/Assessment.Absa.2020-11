using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace Phonebook.Api.Infrastructure.Models.Bases {

    public abstract class ResponseBase {

        /// <summary>
        /// HTTP status code or a numeric code corresponding to the error in an error result
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        [DefaultValue(200)]
        [JsonProperty(PropertyName = "StatusCode")]
        public int StatusCode { get; set; } = 200;

        /// <summary>
        /// Status code (i.e. SUCCESS, FAIL or ERROR)
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; } = "SUCCESS";

        /// <summary>
        /// A meaningful, end-user-readable (or at the least log-worthy) message, 
        /// explaining the current status (e.g. what went wrong in an error result).
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; } = "";

        /// <summary>
        /// Helps to identify the originating service
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        [JsonProperty(PropertyName = "Source")]
        public string Source { get; set; }
    }
}
