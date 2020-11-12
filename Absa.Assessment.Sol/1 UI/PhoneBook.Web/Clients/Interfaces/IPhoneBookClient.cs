using System.Threading.Tasks;

using PhoneBook.Api.Domain.Models.Request;

using PhoneBook.Web.Models;

namespace PhoneBook.Web.Clients.Interfaces {

    public interface IPhoneBookClient {

        /// <summary>
        /// Saves the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        Task<string> SaveEntry(string apiVersion, EntryRequest request);

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<SearchResultResponse> GetPhoneBookEntries(string apiVersion, string name);

        /// <summary>
        /// Searches the entries.
        /// </summary>
        /// <param name="apiVersion">The API version.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        Task<SearchResultResponse> SearchEntries(string apiVersion, string query);
    }
}
