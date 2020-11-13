using System;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using FluentValidation.AspNetCore;

using PhoneBook.Api.Domain.Models.Request;
using PhoneBook.Api.Infrastructure.Exceptions;
using PhoneBook.Api.Infrastructure.Extensions;
using PhoneBook.Api.Infrastructure.Results;
using PhoneBook.Api.Services.Interfaces;

using Log = PhoneBook.Api.Domain.Logging.LoggingConstants;

namespace PhoneBook.Api.Controllers {
    
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class PhoneBookController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly ILogger<PhoneBookController> _logger;

        private readonly IPhoneBookService _phoneBookService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneBookController"/> class.
        /// </summary>
        public PhoneBookController(IConfiguration configuration, ILogger<PhoneBookController> logger, IPhoneBookService phoneBookService) {

            _configuration = configuration;
            _logger = logger;

            _phoneBookService = phoneBookService;
        }

        /// <summary>
        /// Saves the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        /// <exception cref="ApiArgumentException">Parameter cannot be null or empty - Name</exception>
        [HttpPost]
        [Route("entries/save")]
        public IActionResult SaveEntry([CustomizeValidator(RuleSet = "default")][FromBody] EntryRequest entry) {

            string correlationId = Guid.NewGuid().ToString();

            try {
                var result = _phoneBookService.SaveEntry(correlationId, entry);

                return new SubmissionSucceededResult(result, result ? "Entry saved successfully" : $"Unable to save new entry for {entry.Name}", ToString());
            }
            catch (Exception ex) {

                _logger.LogError(new EventId((int)Log.EventId.SubmissionFailed), ex, Log.Template,
                    Log.CheckPoint.Standard.ToString(), ToString(), correlationId, (int)Log.EventId.SubmissionFailed, Log.EventId.SubmissionFailed.ToString(),
                    ToString(), correlationId, Log.Status.FAILED.ToString(), ex.InnermostException().Message);

                return new SubmissionFailedResult(Log.EventId.SubmissionFailed.ToString(), ex.InnermostException().Message, ToString(), exception: ex);
            }            
        }

        /// <summary>
        /// Gets the entries for the specified user
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="ApiArgumentException">Parameter cannot be null or empty - name</exception>
        [HttpGet]
        [Route("entries/get/{name}")]
        public IActionResult GetEntries(string name) {

            if (string.IsNullOrEmpty(name)) {
                throw new ApiArgumentException("Parameter cannot be null or empty", nameof(name));
            }

            string correlationId = Guid.NewGuid().ToString();            

            try {
                var results = _phoneBookService.GetEntries(correlationId, name);

                return new SubmissionSucceededResult(results, results.Count > 0 ? "Phonebook entries retrieved successfully" : $"Unable to retrieve phonebook entries for {name}", ToString());
            }
            catch (Exception ex) {

                _logger.LogError(new EventId((int)Log.EventId.SubmissionFailed), ex, Log.Template,
                    Log.CheckPoint.Standard.ToString(), ToString(), correlationId, (int)Log.EventId.SubmissionFailed, Log.EventId.SubmissionFailed.ToString(),
                    ToString(), correlationId, Log.Status.FAILED.ToString(), ex.Message);

                return new SubmissionFailedResult(Log.EventId.SubmissionFailed.ToString(), ex.Message, ToString(), exception: ex);
            }            
        }

        /// <summary>
        /// Searches the entries.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="ApiArgumentException">Parameter cannot be null or empty - query</exception>
        [HttpGet]
        [Route("entries/search/{query}")]
        public IActionResult SearchEntries(string query) {

            if (string.IsNullOrEmpty(query)) {
                throw new ApiArgumentException("Parameter cannot be null or empty", nameof(query));
            }

            string correlationId = Guid.NewGuid().ToString();

            try {
                var results = _phoneBookService.SearchEntries(correlationId, query);

                return new SubmissionSucceededResult(results, results.Count > 0 ? "Phonebook entries retrieved successfully" : $"Unable to retrieve phonebook entries for {query}", ToString());
            }
            catch (Exception ex) {

                _logger.LogError(new EventId((int)Log.EventId.SubmissionFailed), ex, Log.Template,
                    Log.CheckPoint.Standard.ToString(), ToString(), correlationId, (int)Log.EventId.SubmissionFailed, Log.EventId.SubmissionFailed.ToString(),
                    ToString(), correlationId, Log.Status.FAILED.ToString(), ex.Message);

                return new SubmissionFailedResult(Log.EventId.SubmissionFailed.ToString(), ex.Message, ToString(), exception: ex);
            }
        }
    }
}
