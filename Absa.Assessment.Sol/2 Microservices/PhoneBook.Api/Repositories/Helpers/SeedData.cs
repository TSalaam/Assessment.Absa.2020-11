using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PhoneBook.Api.Repositories.Helpers {

    /// <summary>
    /// 
    /// </summary>
    public static class SeedData {

        /// <summary>
        /// Initializes the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider) {

            using (var context = new PhoneBookContext(

                serviceProvider.GetRequiredService<DbContextOptions<PhoneBookContext>>())) {

                //if (context.PhoneBooks.Any()) {
                //    return;   // Database has been seeded
                //}

                context.PhoneBooks.AddRange(
                     new Domain.Models.PhoneBook {
                         Name = "Default user",
                         Entries = null
                     }
                );
                context.Entries.AddRange(
                     new Domain.Models.Entry {
                         Name = "Default user",
                         PhoneNumber = string.Empty
                     }
                );

                context.SaveChanges();
            }
        }
    }
}
