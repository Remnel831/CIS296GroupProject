using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private Repository<Incident> Incidents { get; set; }
        private Repository<Technician> Technicians { get; set; }
        private Repository<Product> Products { get; set; }
        private Repository<Customer> Customers { get; set; }


        public IncidentController(SportsProContext ctx)
        {
            Incidents = new Repository<Incident>(ctx);
            Technicians = new Repository<Technician>(ctx);
            Products = new Repository<Product>(ctx);
            Customers = new Repository<Customer>(ctx);
        }

        //Add
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActiveTab = "Incident";
            ViewBag.Action = "Add";
            IncidentViewModel viewModel = new(ViewBag.Action)
            {
                Incident = new Incident(),
                Technicians = Technicians.List(new QueryOptions<Technician>()),
                Customers = Customers.List(new QueryOptions<Customer>()),
                Products = Products.List(new QueryOptions<Product>())
            };
            return View("Edit", viewModel);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveTab = "Incident";
            ViewBag.Action = "Edit";
            var incident = Incidents.Get(id);

            if (incident == null)
            {
                incident = new Incident();
            }

            IncidentViewModel viewModel = new(ViewBag.Action)
            {
                Incident = incident,
                Technicians = Technicians.List(new QueryOptions<Technician>()),
                Customers = Customers.List(new QueryOptions<Customer>()),
                Products = Products.List(new QueryOptions<Product>())
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel incidentVM)
        {
            ViewBag.ActiveTab = "Incident";

            int technicianId = HttpContext.Session.GetInt32("TechnicianID").GetValueOrDefault();
            if (ModelState.IsValid)
            {
                if (incidentVM.Incident.IncidentID == 0)
                {
                    Incidents.Insert(incidentVM.Incident);
                }
                else
                {
                    Incidents.Update(incidentVM.Incident);
                }

                Incidents.Save();

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
                if (technicianId > 0)
                {
                    var incident = Incidents.Get(incidentVM.Incident.IncidentID);
                    if (incident != null)
                    {
                        incident.DateClosed = incidentVM.Incident.DateClosed;
                        incident.Description = incidentVM.Incident.Description;
                        incidentVM.Incident = incident;
                        Incidents.Update(incidentVM.Incident);

                        Incidents.Save();
                        return RedirectToAction("List", "TechIncident", new { Id = technicianId });
                    }
                }

                ViewBag.Action = (incidentVM.Incident.IncidentID == 0) ? "Add" : "Edit";
                IncidentViewModel viewModel = new(ViewBag.Action)
                {
                    Incident = new Incident(),
                    Technicians = Technicians.List(new QueryOptions<Technician>()),
                    Customers = Customers.List(new QueryOptions<Customer>()),
                    Products = Products.List(new QueryOptions<Product>())
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

            var incidentOptions = new QueryOptions<Incident>
            {
                Includes = "Customer,Product",
                OrderBy = i => i.DateOpened
            };

            IQueryable<Incident> query = Incidents.List(incidentOptions).AsQueryable();
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
            var incident = Incidents.Get(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            ViewBag.Action = "Delete";
            Incidents.Delete(incident);
            Incidents.Save();
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
