using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;

using PhoneBook.Api.Domain.Models;
using PhoneBook.Api.Domain.Models.Request;
using PhoneBook.Api.Repositories.Interfaces;

namespace PhoneBook.Api.Repositories {

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PhoneBook.Api.Repositories.Interfaces.IPhoneBookRepository" />
    public class PhoneBookRepository : IPhoneBookRepository {

        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneBookRepository"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public PhoneBookRepository(IConfiguration config) {
            this.config = config;
        }

        /// <summary>
        /// Saves the entry.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public bool SaveEntry(EntryRequest request) {

            var optionsBuilder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<PhoneBookContext>();

            using (var db = new PhoneBookContext(optionsBuilder.Options)) {

                var entry = new Entry {
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber
                };

                db.Entries.Add(entry);

                int count = db.SaveChanges();

                return (count > 0);
            }
        }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<Entry> GetEntries(string name) {

            var optionsBuilder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<PhoneBookContext>();

            using (var db = new PhoneBookContext(optionsBuilder.Options)) {

                var entries = db.Entries
                    .Where(b => b.Name.Contains(name))
                    .OrderBy(b => b.Name)
                    .ToList();

                return entries;
            }
        }

        /// <summary>
        /// Searches the entries.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public List<Entry> SearchEntries(string query) {

            var optionsBuilder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<PhoneBookContext>();

            using (var db = new PhoneBookContext(optionsBuilder.Options)) {

                var entries = db.Entries
                    .Where(b => b.PhoneNumber.Contains(query))
                    .OrderBy(b => b.Name)
                    .ToList();

                return entries;
            }
        }
    }
}