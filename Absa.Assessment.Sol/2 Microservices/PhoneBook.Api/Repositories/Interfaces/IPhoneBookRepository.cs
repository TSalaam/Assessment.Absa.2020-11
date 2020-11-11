using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PhoneBook.Api.Domain.Models;
using PhoneBook.Api.Domain.Models.Request;

namespace PhoneBook.Api.Repositories.Interfaces {

    /// <summary>
    /// 
    /// </summary>
    public interface IPhoneBookRepository {

        /// <summary>
        /// Saves the entry.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        bool SaveEntry(EntryRequest request);

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        List<Entry> GetEntries(string name);

        /// <summary>
        /// Searches the entries.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        List<Entry> SearchEntries(string query);
    }
}
