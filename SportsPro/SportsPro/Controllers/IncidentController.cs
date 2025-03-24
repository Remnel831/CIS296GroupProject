using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext Context { get; set; }


        public IncidentController(SportsProContext ctx) => Context = ctx;

        //Add
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActiveTab = "Incident";
            ViewBag.Action = "Add";
            IncidentViewModel viewModel = new(ViewBag.Action)
            {
                Incident = new Incident(),
                Technicians = Context.Technicians.ToList(),
                Customers = Context.Customers.ToList(),
                Products = Context.Products.ToList()
            };
            return View("Edit", viewModel);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveTab = "Incident";
            ViewBag.Action = "Edit";
            var incident = Context.Incidents.Find(id);

            if (incident == null)
            {
                incident = new Incident();
            }

            IncidentViewModel viewModel = new(ViewBag.Action)
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
                }
                else
                {
                    Context.Incidents.Update(incidentVM.Incident);
                }

                Context.SaveChanges();

                int technicianId = HttpContext.Session.GetInt32("TechnicianID").GetValueOrDefault();
                if (technicianId > 0)
                {
                    return RedirectToAction("List", "TechIncident", new { Id = technicianId });
                }
                else
                {
                    return RedirectToAction("List", "Incident");
                }

            }
            else
            {
                ViewBag.Action = (incidentVM.Incident.IncidentID == 0) ? "Add" : "Edit";
                IncidentViewModel viewModel = new(ViewBag.Action)
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
        [Route("[controller]s/{id?}")]
        public ViewResult List(IncidentListViewModel model)
        {
            ViewBag.ActiveTab = "Incident";
            HttpContext.Session.SetInt32("TechnicianID", 0);

            string? filter = HttpContext.Session.GetString("Filter");
            if (string.IsNullOrEmpty(filter))
            {
                filter = "All";
            }
            model.IncidentFilter = filter;

            IQueryable<Incident> query = Context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .OrderBy(i => i.DateOpened);
            ViewBag.ActiveFilter = IncidentFilterEnum.All.ToString();
            if (model.IncidentFilter == IncidentFilterEnum.Open.ToString())
            {
                ViewBag.ActiveFilter = model.IncidentFilter;
                query = query.Where(i => i.DateClosed == null);
            }
            else if (model.IncidentFilter == IncidentFilterEnum.Unassigned.ToString())
            {
                ViewBag.ActiveFilter = model.IncidentFilter;
                query = query.Where(i => i.TechnicianID == -1);
            }

            model.Incidents = query.ToList();

            return View(model);
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

        [HttpPost]
        public IActionResult Filter(string filter = "All")
        {
            HttpContext.Session.SetString("Filter", filter);
            return RedirectToAction("List", new { id = filter });
        }
    }
}
