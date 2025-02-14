using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Incident
    {
		public int IncidentID { get; set; }
        [Required(ErrorMessage = "Please enter the title of the incident.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please describe what is wrong.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the date the the incident happend.")]
        public DateTime DateOpened { get; set; } = DateTime.Now;
        public DateTime? DateClosed { get; set; } = null;

        [Required(ErrorMessage = "internal error, please contact site support")]
        public int CustomerID { get; set; }                   // foreign key property
		public Customer Customer { get; set; } = null!;       // navigation property

        [Required(ErrorMessage = "internal error, please contact site support")]
        public int ProductID { get; set; }                    // foreign key property
		public Product Product { get; set; } = null!;         // navigation property

		[Required(ErrorMessage = "internal error, please contact site support")]
		public int TechnicianID { get; set; }                 // foreign key property 
		public Technician? Technician { get; set; } = null!;   // navigation property

		
	}
}