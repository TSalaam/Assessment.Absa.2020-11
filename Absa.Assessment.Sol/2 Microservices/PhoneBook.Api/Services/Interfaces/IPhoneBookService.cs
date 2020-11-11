using System.Collections.Generic;

using PhoneBook.Api.Domain.Models;
using PhoneBook.Api.Domain.Models.Request;

namespace PhoneBook.Api.Services.Interfaces {

    /// <summary>
    /// 
    /// </summary>
    public interface IPhoneBookService {

        /// <summary>
        /// Saves the entry.
        /// </summary>
        /// <param name="correlationId">Unique identifier of the message</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        bool SaveEntry(string correlationId, EntryRequest request);

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="correlationId">Unique identifier of the message</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        List<Entry> GetEntries(string correlationId, string name);

        /// <summary>
        /// Searches the entries.
        /// </summary>
        /// <param name="correlationId">Unique identifier of the message</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        List<Entry> SearchEntries(string correlationId, string query);
    }
}
