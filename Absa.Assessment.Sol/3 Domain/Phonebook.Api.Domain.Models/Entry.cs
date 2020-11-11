using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Domain.Models {

    public class Entry {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
