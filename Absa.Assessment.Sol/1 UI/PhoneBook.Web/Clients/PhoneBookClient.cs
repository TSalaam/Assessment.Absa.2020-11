using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using PhoneBook.Api.Domain.Models.Request;
using PhoneBook.Api.Infrastructure.Results;

using PhoneBook.Web.Configuration;
using PhoneBook.Web.Clients.Helpers;
using PhoneBook.Web.Clients.Interfaces;

using Log = PhoneBook.Api.Domain.Logging.LoggingConstants;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Clients {

    public class PhoneBookClient : IPhoneBookClient {

        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger<PhoneBookClient> _logger;

        private readonly HttpClient _apiClient;

        private readonly string _phonebookSvcBaseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneBookClient"/> class.
        /// </summary>
        /// <param name="settings">Represents a set of key/value application configuration properties</param>
        /// <param name="logger">The logger</param>
        /// <param name="httpClient">The HTTP client.</param>
        public PhoneBookClient(IOptions<AppSettings> settings, ILogger<PhoneBookClient> logger, HttpClient httpClient) {

            _settings = settings;
            _apiClient = httpClient;
            _logger = logger;

            _phonebookSvcBaseUrl = $"{_settings.Value.ServiceUrls.PhoneBookUrl}/api/PhoneBook/";
        }

        /// <summary>
        /// Saves the entry.
        /// </summary>
        /// <param name="apiVersion">The API version.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<string> SaveEntry(string apiVersion, EntryRequest request) {

            var uri = Infrastructure.UrlsConfig.PhoneBook.SaveEntry(_phonebookSvcBaseUrl).Replace("api", $"api/{apiVersion}");

            var responseMessage = await _apiClient.PostAsync(uri, ApiHelper.CreateHttpContent(request));

            if (!responseMessage.IsSuccessStatusCode) {
                return "Unable to connect to PhoneBook Service";
            }

            var streamTask = responseMessage.Content.ReadAsStreamAsync();

            var serializer = new DataContractJsonSerializer(typeof(string));

            var response = serializer.ReadObject(await streamTask) as string;

            return response;
        }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="apiVersion"></param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<SearchResultResponse> GetPhoneBookEntries(string apiVersion, string name) {

            var uri = Infrastructure.UrlsConfig.PhoneBook.GetEntries(_phonebookSvcBaseUrl, name).Replace("api", $"api/{apiVersion}");

            var dataString = await _apiClient.GetStringAsync(uri);

            var response = JsonConvert.DeserializeObject<SearchResultResponse>(dataString);

            return response;
        }

        /// <summary>
        /// Searches the entries.
        /// </summary>
        /// <param name="apiVersion">The API version.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public async Task<SearchResultResponse> SearchEntries(string apiVersion, string query) {

            var uri = Infrastructure.UrlsConfig.PhoneBook.SearchEntries(_phonebookSvcBaseUrl, query).Replace("api", $"api/{apiVersion}");

            var dataString = await _apiClient.GetStringAsync(uri);

            var response = JsonConvert.DeserializeObject<SearchResultResponse>(dataString);

            return response;
        }
    }
}