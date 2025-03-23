namespace SportsPro.Models
{
    public class IncidentListViewModel
    {
        public string IncidentFilter { get; set; } = IncidentFilterEnum.All.ToString();
        public List<Incident> Incidents { get; set; } = null!;

        public IncidentListViewModel()
        {
            IncidentFilter = "All";
        }
    }

    public enum IncidentFilterEnum
    {
        All,
        Open,
        Unassigned
    }
}
