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

        [Route("Registrations/GetCustomer")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("CustomerID").HasValue)
            {
                return RedirectToAction("List", new { SelectedCustomerID = HttpContext.Session.GetInt32("CustomerID").Value });
            }

            var Customers = Context.Customers.Where(item => item.CustomerID > 0).ToList();
            ViewBag.Title = "Get Registrations";
            CustomerRegistrationsViewModel model = new(Customers);
            return View(model);
        }

        [HttpPost("Registrations/GetCustomer")]
        public IActionResult Index(int SelectedCustomerID)
        {
            return RedirectToAction("List", new { SelectedCustomerID });
        }

        [HttpGet]
        [Route("Registrations/{SelectedCustomerID:int}")]
        public IActionResult List(int SelectedCustomerID)
        {
            var customer = Context.Customers
            .Include(c => c.Products)
            .FirstOrDefault(c => c.CustomerID == SelectedCustomerID);
            //var customer = Context.Customers.Where(item => item.CustomerID == SelectedCustomerID).SingleOrDefault();
            var products = customer?.Products.ToList() ?? new List<Product>();
            var allProducts = Context.Products.ToList();

            var model = new CustomerRegistrationsListViewModel(customer, products, allProducts);

            HttpContext.Session.SetInt32("CustomerID", SelectedCustomerID);
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(int selectedProductID)
        {
            int selectedCustomerID = 0;
            if (HttpContext.Session.GetInt32("CustomerID").HasValue)
            {
                selectedCustomerID = HttpContext.Session.GetInt32("CustomerID").Value;
            }
             
            var customer = Context.Customers
                .Include(c => c.Products)
                .FirstOrDefault(c => c.CustomerID == selectedCustomerID);

            var product = Context.Products.Find(selectedProductID);

            // Check to avoid duplicates
            if (!customer.Products.Any(p => p.ProductID == selectedProductID))
            {
                customer.Products.Add(product);
                Context.SaveChanges();
            }

            return RedirectToAction("List", new { selectedCustomerID });
        }

        [HttpPost]
        public IActionResult Delete(int selectedProductID)
        {
            int selectedCustomerID = 0;
            if (HttpContext.Session.GetInt32("CustomerID").HasValue)
            {
                selectedCustomerID = HttpContext.Session.GetInt32("CustomerID").Value;
            }

            var customer = Context.Customers
                .Include(c => c.Products)
                .FirstOrDefault(c => c.CustomerID == selectedCustomerID);

            var product = Context.Products.Find(selectedProductID);

            // Check to avoid duplicates
            if (customer.Products.Any(p => p.ProductID == selectedProductID))
            {
                customer.Products.Remove(product);
                Context.SaveChanges();
            }

            return RedirectToAction("List", new { selectedCustomerID });
        }
    }
}
