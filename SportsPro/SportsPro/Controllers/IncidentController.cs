using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext Context { get; set; }


        public IncidentController(SportsProContext ctx)
        {
            Context = ctx;
        }
        //Add

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActiveTab = "Incident";
            IncidentViewModel viewModel = new()
            {
                Incident = new Incident(),
                Technicians = Context.Technicians.ToList(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList()
            };
            ViewBag.Action = "Add";
            return View("Edit", viewModel);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveTab = "Incident";
            ViewBag.Action = "Edit";
            var incident = Context.Incidents.Find(id);

            IncidentViewModel viewModel = new();

            if (incident == null)
            {
                incident = new Incident();
            }

            viewModel = new()
            {
                Incident = incident,
                Technicians = Context.Technicians.ToList(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel incidentVM)
        {
            ViewBag.ActiveTab = "Incident";
            if (ModelState.IsValid)
            {
                if (incidentVM.Incident.IncidentID == 0)
                {
                    Context.Incidents.Add(incidentVM.Incident);
                    Context.SaveChanges();
                    return RedirectToAction("List", "Incident");
                }
                else
                {
                    Context.Incidents.Update(incidentVM.Incident);
                    Context.SaveChanges();
                    return RedirectToAction("List", "Incident");
                }

            }
            else
            {
                ViewBag.Action = (incidentVM.Incident.IncidentID == 0) ? "Add" : "Edit";
                IncidentViewModel viewModel = new()
                { 
                    Incident = new Incident(),
                    Technicians = Context.Technicians.ToList(),
                    Customers = Context.Customers.ToList(),
                    Products = Context.Products.ToList()
                };
                return View(viewModel);
            }
        }

        //List
        [Route("incidents")]
        public IActionResult List()
        {
            ViewBag.ActiveTab = "Incident";
            List<Incident> incidents = Context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .OrderBy(i => i.DateOpened)
                .ToList();

            return View(incidents);
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.ActiveTab = "Incident";
            var incident = Context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            ViewBag.Action = "Delete";
            Context.Incidents.Remove(incident);
            Context.SaveChanges();
            return RedirectToAction("List", "Incident");
        }
    }
}
