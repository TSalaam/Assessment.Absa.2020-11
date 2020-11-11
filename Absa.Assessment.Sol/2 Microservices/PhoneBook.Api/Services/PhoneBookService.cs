using System;
using System.Collections.Generic;

using PhoneBook.Api.Domain.Models;
using PhoneBook.Api.Domain.Models.Request;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services.Interfaces;

namespace PhoneBook.Api.Services {

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Services.Interfaces.IPhoneBookService" />
    public class PhoneBookService : IPhoneBookService {

        private readonly IPhoneBookRepository _phoneBookRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneBookService"/> class.
        /// </summary>
        /// <param name="phoneBookRepository">The PhoneBook repository.</param>
        public PhoneBookService(IPhoneBookRepository phoneBookRepository) {
            _phoneBookRepository = phoneBookRepository;
        }

        /// <summary>
        /// Saves the entry.
        /// </summary>
        /// <param name="correlationId">Unique identifier of the message</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public bool SaveEntry(string correlationId, EntryRequest request) => _phoneBookRepository.SaveEntry(request);

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="correlationId">Unique identifier of the message</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<Entry> GetEntries(string correlationId, string name) => _phoneBookRepository.GetEntries(name);

        /// <summary>
        /// Searches the entries.
        /// </summary>
        /// <param name="correlationId">Unique identifier of the message</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public List<Entry> SearchEntries(string correlationId, string query) => _phoneBookRepository.SearchEntries(query);
    }
}
