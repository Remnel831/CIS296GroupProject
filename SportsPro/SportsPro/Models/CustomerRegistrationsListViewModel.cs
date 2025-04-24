namespace SportsPro.Models
{
    public class CustomerRegistrationsListViewModel
    {
        public Customer Customer { get; set; } = new Customer();

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public IEnumerable<Product> AllProducts { get; set; } = new List<Product>();

        public int SelectedProductID { get; set; }

        public CustomerRegistrationsListViewModel() { }

        public CustomerRegistrationsListViewModel(Customer inCustomer, IEnumerable<Product> inProducts, IEnumerable<Product> allProducts)
        {
            Customer = inCustomer;
            Products = inProducts;
            AllProducts = allProducts;
        }
    }
}
