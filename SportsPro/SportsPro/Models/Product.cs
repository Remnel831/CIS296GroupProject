using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Please enter a Product Code.")]
        public string ProductCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a Product Name.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a Yearly Price.")]
        [Column(TypeName = "decimal(8,2)")]
        [Range(0.01, int.MaxValue, ErrorMessage = "The yearly price must be more than $0.")]
        public decimal YearlyPrice { get; set; }
		public DateTime ReleaseDate { get; set; } = DateTime.Now;
	}
}
