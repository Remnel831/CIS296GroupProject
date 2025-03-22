namespace SportsPro.Models
{
    public class TechIncidentListViewModel
    {
        public Technician Technician;

        public List<Incident> Incidents;
        


        // Constructors
        public TechIncidentListViewModel() { }

        public TechIncidentListViewModel(Technician inTech, List<Incident> inIncidents)
        {
            Technician = inTech;
            Incidents = inIncidents;
        }

    }
}
