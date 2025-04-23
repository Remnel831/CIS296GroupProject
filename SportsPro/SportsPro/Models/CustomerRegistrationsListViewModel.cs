namespace SportsPro.Models
{
    public class CustomerRegistrationsListViewModel
    {
        public Customer Customer { get; set; } = new Customer();

        public List<Product> Products { get; set; } = new List<Product>();

        public CustomerRegistrationsListViewModel() { }

        public CustomerRegistrationsListViewModel(Customer inCustomer, List<Product> inProducts)
        {
            Customer = inCustomer;
            Products = inProducts;
        }
    }
}
