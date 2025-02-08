namespace SportsPro.Models
{
    public class IncidentViewModel
    {
        public Incident Incident { get; set; } = null!;
        public IEnumerable<Technician> Technicians { get; set; } = new List<Technician>();
        public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public IncidentViewModel() { }
    }
}
