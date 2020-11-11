using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Domain.Models {

    public class PhoneBook {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
