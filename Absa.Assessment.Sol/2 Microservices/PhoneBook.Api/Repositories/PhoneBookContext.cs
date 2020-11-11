using System.IO;

using Microsoft.Extensions.Configuration;

using Microsoft.EntityFrameworkCore;

using Models = PhoneBook.Api.Domain.Models;

namespace PhoneBook.Api.Repositories {

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class PhoneBookContext : DbContext {

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneBookContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options)
            : base(options) { }

        /// <summary>
        /// Gets or sets the phone books.
        /// </summary>
        /// <value>
        /// The phone books.
        /// </value>
        public DbSet<Models.PhoneBook> PhoneBooks { get; set; }

        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        /// <value>
        /// The entries.
        /// </value>
        public DbSet<Models.Entry> Entries { get; set; }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            if (!optionsBuilder.IsConfigured) {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}
