using System;

namespace PhoneBook.Web.Models {

    public class DefaultResponse {
        public object result { get; set; }
        public int statusCode { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string source { get; set; }
    }
}
