using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
		public int ProductID { get; set; }
        //correctly spaces "Product code"
        [Required(ErrorMessage = "The Product code field is required")]
		public string ProductCode { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(8,2)")]
        //account for negative entry
        [Range(0.01, 99999999.99, ErrorMessage = "Yearly price must be a positive number.")]
        public decimal YearlyPrice { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
	}
}
