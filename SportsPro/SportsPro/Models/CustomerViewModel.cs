namespace SportsPro.Models
{
    public class CustomerViewModel
    {
        public Customer Customer { get; set; } = null!;
        public IEnumerable<Country> Countries { get; set; } = new List<Country>();


        // Constructors
        public CustomerViewModel() { }
        public CustomerViewModel(Customer customer, IEnumerable<Country> countries)
        {
            Customer = customer;
            Countries = countries;
        }




    }


}
