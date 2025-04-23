using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class CustomerRegistrationsViewModel
    {
        public List<Customer> Customers { get; set; } = null!;
        [Required(ErrorMessage = "Please select a customer.")]
        public int SelectedCustomerID { get; set; }

        public CustomerRegistrationsViewModel() { }
        public CustomerRegistrationsViewModel(List<Customer> inCustomers)
        {
            if (inCustomers != null)
            {
                Customers = inCustomers;
            }
            else
            {
                Customers = new List<Customer>();
            }
        }
    }
}
