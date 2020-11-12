using System;

namespace PhoneBook.Web.Infrastructure {

    public class UrlsConfig {

        /// <summary>
        /// 
        /// </summary>
        public static class PhoneBook {

            public static string SaveEntry(string baseUri) {
                return $"{baseUri}entries/save";
            }

            public static string GetEntries(string baseUri, string name) {
                return $"{baseUri}entries/get/{name}";
            }

            public static string SearchEntries(string baseUri, string query) {
                return $"{baseUri}entries/search/{query}";
            }
        }
    }
}
