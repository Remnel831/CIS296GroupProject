using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class TechIncidentIndexViewModel
    {
        public IEnumerable<Technician>  Technicians { get; set; } = null!;
        [Required(ErrorMessage = "Please select a technician.")]
        public int SelectedTechnicianID { get; set; }



        // Constructors
        public TechIncidentIndexViewModel() { }
        public TechIncidentIndexViewModel(IEnumerable<Technician> inTechnicians)
        {
            if (inTechnicians != null)
            {
                Technicians = inTechnicians;
            }
            else
            {
                Technicians = new List<Technician>();
            }
        }
    }
}
