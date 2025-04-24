using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter the customer's first name.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the customer's last name.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the customer's address.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer's address must be between 1 and 50 characters.")]
        public string Address { get; set; } = string.Empty;
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer's city must be between 1 and 50 characters.")]
        [Required(ErrorMessage = "Please enter the customer's City.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the customer's State.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer's state must be between 1 and 50 characters.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the customer's postal code.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Customer's zip code must be between 1 and 20 characters.")]
        public string PostalCode { get; set; } = string.Empty;
       
        [RegularExpression(@"^\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Phone numbers must match the format (555)555-5555")]
        public string? Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [NewEmail]
        public string? Email { get; set; }

        [ValidCountry]
        public string CountryID { get; set; } = string.Empty;   // foreign key property
        public Country? Country { get; set; } = null!;           // navigation property

        public string FullName => FirstName + " " + LastName;   // read-only property

        [ForeignKey("ProductID")]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}