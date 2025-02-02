using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Technician
    {
		public int TechnicianID { get; set; }
        [Required(ErrorMessage = "Please enter the Technician's Name.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the Technician's Email.")]
        [RegularExpression("^(?!.*\\.\\.)(?!\\.)[a-zA-Z0-9]([a-zA-Z0-9_+\\-\\.]{0,62}[a-zA-Z0-9])?[@](?=.{4,255}$)[a-zA-Z0-9]+([.-][a-zA-Z0-9]+)*\\.(xn--)?[a-zA-Z0-9]{2,}$", ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter the Technician's Phone Number.")]
        [RegularExpression("^\\s*(\\+1\\s*)?(?:\\(?\\d{3}\\)?([\\s.-])*?)?\\d{3}[\\s.-]*?\\d{4}\\s*$", ErrorMessage = "Please enter a valid Phone number")]
        public string Phone { get; set; } = string.Empty;
    }
}
