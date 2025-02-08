using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter the customer's first name.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the customer's last name.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the customer's address.")]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the customer's City.")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the customer's State.")]
        public string State { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the customer's postal code.")]
        public string PostalCode { get; set; } = string.Empty;
        [RegularExpression("^\\s*(\\+1\\s*)?(?:\\(?\\d{3}\\)?([\\s.-])*?)?\\d{3}[\\s.-]*?\\d{4}\\s*$", ErrorMessage = "Please enter a valid Phone number")]
        public string? Phone { get; set; }
        [RegularExpression("^(?!.*\\.\\.)(?!\\.)[a-zA-Z0-9]([a-zA-Z0-9_+\\-\\.]{0,62}[a-zA-Z0-9])?[@](?=.{4,255}$)[a-zA-Z0-9]+([.-][a-zA-Z0-9]+)*\\.(xn--)?[a-zA-Z0-9]{2,}$", ErrorMessage = "Please enter a valid email.")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="internal error, please contact site support")]
        public string CountryID { get; set; } = string.Empty;   // foreign key property
        public Country? Country { get; set; } = null!;           // navigation property

        public string FullName => FirstName + " " + LastName;   // read-only property
	}
}