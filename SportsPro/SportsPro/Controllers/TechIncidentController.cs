using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class TechIncidentController : Controller
	{
        private SportsProContext Context { get; set; }

        public TechIncidentController(SportsProContext ctx)
        {
            Context = ctx;
        }

        public IActionResult Index()
		{
            var technicians = Context.Technicians.Where(item => item.TechnicianID > 0).ToList();
            ViewBag.Title = "Get Technicians";
            TechIncidentIndexViewModel model = new TechIncidentIndexViewModel(technicians);
            return View(model);
		}


        [HttpPost("TechIncident/List/{id}")]
        public IActionResult List(int SelectedTechnicianID)
        {
            var technician = Context.Technicians.Where(item => item.TechnicianID == SelectedTechnicianID).SingleOrDefault();
            var incidentsByTech = Context.Incidents
                .Where(item => item.TechnicianID == SelectedTechnicianID  && item.DateClosed == null)
                .Include(item => item.Customer)
                .Include(item => item.Product)
                .OrderBy(item => item.DateOpened)
                .ToList();

            if (technician == null)
                return NotFound(); 
            var model = new TechIncidentListViewModel(technician, incidentsByTech);

            HttpContext.Session.SetInt32("TechnicianID", SelectedTechnicianID);
            return View(model);
        }
    }
}
