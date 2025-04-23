namespace SportsPro.Models
{
    public class Registration
    {
        public int? CustomerID { get; set; }

        public int? ProductID { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual Product? Product { get; set; }
    }
}
