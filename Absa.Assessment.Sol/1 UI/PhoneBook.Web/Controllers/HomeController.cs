using System;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using PhoneBook.Api.Domain.Models.Request;

using PhoneBook.Web.Clients;
using PhoneBook.Web.Clients.Interfaces;
using PhoneBook.Web.Configuration;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Controllers {

    public class HomeController : Controller {

        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly ILogger<HomeController> _logger;

        private readonly IPhoneBookClient _phoneBookClient;

        public HomeController(IOptionsSnapshot<AppSettings> settings, ILogger<HomeController> logger, IPhoneBookClient phoneBookClient) {

            _settings = settings;
            _logger = logger;

            _phoneBookClient = phoneBookClient;
        }

        internal string ApiVersion_Major {
            get { return "v1"; }
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult About() {

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult PhoneBook() {

            ViewData["Message"] = "Manage your phonebook.";

            return View();
        }

        [HttpGet]
        public IActionResult PhoneBookData(string phonebookName) {
            try {
                var result = _phoneBookClient.GetPhoneBookEntries(ApiVersion_Major, phonebookName).Result;

                return new OkObjectResult(new {
                    Message = "Ok",
                    Result = result
                });
            }
            catch (Exception ex) {

                _logger.LogError(ex, ex.Message, null);

                return new NotFoundObjectResult(new {
                    Message = "Not Found",
                    Error = ex.Message,
                    CurrentDate = DateTime.Now
                });
            }
        }

        [HttpGet]
        public IActionResult SaveEntry(string name, string phoneNumber) {
            try {
                var request = new EntryRequest {
                    Name = name,
                    PhoneNumber = phoneNumber
                };

                var result = _phoneBookClient.SaveEntry(ApiVersion_Major, request).Result;

                string message = "Error";

                //if (result.Status == "SUCCESS")
                //    message = "Ok";

                return new OkObjectResult(new {
                    Message = message,
                    Result = result
                });
            }
            catch (Exception ex) {

                _logger.LogError(ex, ex.Message, null);

                return new BadRequestObjectResult(new {
                    Message = "Error while saving phonebook entry",
                    Error = ex.Message,
                    CurrentDate = DateTime.Now
                });
            }
        }

        [HttpGet]
        public IActionResult SearchPhonebookEntries(string query) {
            try {
                var result = _phoneBookClient.SearchEntries(ApiVersion_Major, query).Result;

                return new OkObjectResult(new {
                    Message = "Ok",
                    Result = result
                });
            }
            catch (Exception ex) {

                _logger.LogError(ex, ex.Message, null);

                return new NotFoundObjectResult(new {
                    Message = "Not Found",
                    Error = ex.Message,
                    CurrentDate = DateTime.Now
                });
            }
        }

        public IActionResult Contact() {

            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
