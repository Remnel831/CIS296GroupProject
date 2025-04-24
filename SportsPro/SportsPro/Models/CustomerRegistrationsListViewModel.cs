namespace SportsPro.Models
{
    public class CustomerRegistrationsListViewModel
    {
        public Customer Customer { get; set; } = new Customer();

        public List<Product> Products { get; set; } = new List<Product>();

        public List<Product> AllProducts { get; set; } = new List<Product>();

        public int SelectedProductID { get; set; }

        public CustomerRegistrationsListViewModel() { }

        public CustomerRegistrationsListViewModel(Customer inCustomer, List<Product> inProducts, List<Product> allProducts)
        {
            Customer = inCustomer;
            Products = inProducts;
            AllProducts = allProducts;
        }
    }
}
