using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class TechIncidentController : Controller
	{
        private Repository<Technician> Technicians { get; set; }
        private Repository<Incident> Incidents { get; set; }

        public TechIncidentController(SportsProContext ctx)
        {
            Technicians = new Repository<Technician>(ctx);
            Incidents = new Repository<Incident>(ctx);
        }

        public IActionResult Index()
		{
            var technicianOptions = new QueryOptions<Technician>
            {
                Where = t => t.TechnicianID > 0,
            };
            var technicians = Technicians.List(technicianOptions);
            ViewBag.Title = "Get Technicians";
            TechIncidentIndexViewModel model = new TechIncidentIndexViewModel(technicians);
            return View(model);
		}

        [HttpPost]
        public IActionResult Index(int SelectedTechnicianID)
        {
            return RedirectToAction("List", new { SelectedTechnicianID });
        }

        [HttpGet]
        [Route("TechIncident/List/{SelectedTechnicianID:int}")]
        public IActionResult List(int SelectedTechnicianID)
        {
            var incidentOptions = new QueryOptions<Incident>
            {
                Where = i => i.TechnicianID == SelectedTechnicianID,
                Includes = "Customer,Product",
                OrderBy = i => i.DateOpened
            };
            var technician = Technicians.Get(SelectedTechnicianID);
            var incidentsByTech = Incidents.List(incidentOptions);

            if (technician == null)
                return NotFound(); 
            var model = new TechIncidentListViewModel(technician, incidentsByTech);

            HttpContext.Session.SetInt32("TechnicianID", SelectedTechnicianID);
            return View(model);
        }
    }
}
