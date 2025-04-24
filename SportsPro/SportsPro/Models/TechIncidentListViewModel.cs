namespace SportsPro.Models
{
    public class TechIncidentListViewModel
    {
        public Technician Technician;

        public IEnumerable<Incident> Incidents;
        


        // Constructors
        public TechIncidentListViewModel() { }

        public TechIncidentListViewModel(Technician inTech, IEnumerable<Incident> inIncidents)
        {
            Technician = inTech;
            Incidents = inIncidents;
        }

    }
}
