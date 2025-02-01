using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Technician
    {
		public int TechnicianID { get; set; }
        [Required(ErrorMessage = "Please enter the Technician's Name.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the Technician's Email.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the Technician's Phone Number.")]
        public string Phone { get; set; } = string.Empty;
    }
}
