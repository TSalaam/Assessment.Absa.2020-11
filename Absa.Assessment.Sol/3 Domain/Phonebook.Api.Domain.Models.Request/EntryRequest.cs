using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Api.Domain.Models.Request {

    public class EntryRequest {

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
