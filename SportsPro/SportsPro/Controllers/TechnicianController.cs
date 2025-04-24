using Microsoft.AspNetCore.Mvc;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private Repository<Technician> Technicians { get; set; }

        public TechnicianController(SportsProContext ctx)
        {
            Technicians = new Repository<Technician>(ctx);
        }

        //Add
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActiveTab = "Technician";
            ViewBag.Action = "Add";
            return View("Edit", new Technician());
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveTab = "Technician";
            ViewBag.Action = "Edit";
            var technician = Technicians.Get(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            ViewBag.ActiveTab = "Technician";
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                {
                    Technicians.Insert(technician);
                    Technicians.Save();
                    return RedirectToAction("List", "Technician");
                }
                else
                {
                    Technicians.Update(technician);
                    Technicians.Save();
                    return RedirectToAction("List", "Technician");
                }

            }
            else
            {
                ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
                return View(technician);
            }
        }

        //List
        [Route("technicians")]
        public IActionResult List()
        {
            var technicianOptions = new QueryOptions<Technician>
            {
                Where = t => t.TechnicianID > 0,
            };

            ViewBag.ActiveTab = "Technician";
            ViewBag.Action = "Technicians"; 
            var technicians = Technicians.List(technicianOptions);
            return View(technicians);
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.ActiveTab = "Technician";
            var technician = Technicians.Get(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            ViewBag.Action = "Delete";
            Technicians.Delete(technician);
            Technicians.Save();
            return RedirectToAction("List", "Technician");
        }
    }
}
