using System;
using System.Collections.Generic;

namespace PhoneBook.Web.Models {

    public class SearchResultResponse {
        public List<Result> result { get; set; }
        public int statusCode { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string source { get; set; }
    }

    public class Result {
        public int id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
    }    
}
