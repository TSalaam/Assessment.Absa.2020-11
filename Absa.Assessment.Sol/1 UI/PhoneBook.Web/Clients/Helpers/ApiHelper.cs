using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace PhoneBook.Web.Clients.Helpers {

    /// <summary>
    /// 
    /// </summary>
    public class ApiHelper {

        /// <summary>
        /// Creates the content of the HTTP.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static HttpContent CreateHttpContent<T>(T content) {

            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Gets the microsoft date format settings.
        /// </summary>
        /// <value>
        /// The microsoft date format settings.
        /// </value>
        public static JsonSerializerSettings MicrosoftDateFormatSettings {
            get {
                return new JsonSerializerSettings {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
    }
}
