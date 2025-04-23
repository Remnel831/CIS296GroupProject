using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsProContext Context { get; set; }

        public RegistrationController(SportsProContext ctx)
        {
            Context = ctx;
        }

        public IActionResult Index()
        {
            var Customers = Context.Customers.Where(item => item.CustomerID > 0).ToList();
            ViewBag.Title = "Get Registrations";
            CustomerRegistrationsViewModel model = new CustomerRegistrationsViewModel(Customers);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int SelectedCustomerID)
        {
            return RedirectToAction("List", new { SelectedCustomerID });
        }

        [HttpGet]
        [Route("Registrations/{SelectedCustomerID:int}")]
        public IActionResult List(int SelectedCustomerID)
        {
            var customer = Context.Customers.Where(item => item.CustomerID == SelectedCustomerID).SingleOrDefault();
            var productsByCustomer = Context.Products
                .Include(item => item.Customers
                .Where(item => item.CustomerID == SelectedCustomerID))
                .ToList();
            productsByCustomer ??= new List<Product>();
            var model = new CustomerRegistrationsListViewModel(customer, productsByCustomer);

            HttpContext.Session.SetInt32("CustomerID", SelectedCustomerID);
            return View(model);
        }
    }
}
