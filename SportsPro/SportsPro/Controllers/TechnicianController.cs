using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private SportsProContext Context { get; set; }

        public TechnicianController(SportsProContext ctx)
        {
            Context = ctx;
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
            var technician = Context.Technicians.Find(id);
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
                    Context.Technicians.Add(technician);
                    Context.SaveChanges();
                    return RedirectToAction("List", "Technician");
                }
                else
                {
                    Context.Technicians.Update(technician);
                    Context.SaveChanges();
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
            ViewBag.ActiveTab = "Technician";
            ViewBag.Action = "Technicians"; 
            var technicians = Context.Technicians.Where(item => item.TechnicianID > 0).ToList();
            return View(technicians);
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.ActiveTab = "Technician";
            var technician = Context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            ViewBag.Action = "Delete";
            Context.Technicians.Remove(technician);
            Context.SaveChanges();
            return RedirectToAction("List", "Technician");
        }
    }
}
