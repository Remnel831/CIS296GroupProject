using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private Repository<Product> Products { get; set; }
        private Repository<Customer> Customers { get; set; }

        public RegistrationController(SportsProContext ctx)
        {
            Products = new Repository<Product>(ctx);
            Customers = new Repository<Customer>(ctx);
        }

        [Route("Registrations/GetCustomer")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("CustomerID").HasValue)
            {
                return RedirectToAction("List", new { SelectedCustomerID = HttpContext.Session.GetInt32("CustomerID").Value });
            }

            var customerOptions = new QueryOptions<Customer>
            {
                Where = c => c.CustomerID > 0,
            };

            var customers = Customers.List(customerOptions);
            ViewBag.Title = "Get Registrations";
            CustomerRegistrationsViewModel model = new(customers);
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
            var customerOptions = new QueryOptions<Customer>
            {
                Where = c => c.CustomerID == SelectedCustomerID,
                Includes = "Products"
            };
            var customer = Customers.Get(customerOptions);
            //var customer = Context.Customers.Where(item => item.CustomerID == SelectedCustomerID).SingleOrDefault();
            var products = customer?.Products.ToList() ?? new List<Product>();
            var allProducts = Products.List(new QueryOptions<Product>());

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
             
            var customerOptions = new QueryOptions<Customer>
            {
                Where = c => c.CustomerID == selectedCustomerID,
                Includes = "Products"
            };

            var customer = Customers.Get(customerOptions);

            var product = Products.Get(selectedProductID);

            // Check to avoid duplicates
            if (!customer.Products.Any(p => p.ProductID == selectedProductID))
            {
                customer.Products.Add(product);
                Customers.Save();
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

            var customerOptions = new QueryOptions<Customer>
            {
                Where = c => c.CustomerID == selectedCustomerID,
                Includes = "Products"
            };

            var customer = Customers.Get(customerOptions);

            var product = Products.Get(selectedProductID);

            // Check to avoid duplicates
            if (customer.Products.Any(p => p.ProductID == selectedProductID))
            {
                customer.Products.Remove(product);
                Customers.Save();
            }

            return RedirectToAction("List", new { selectedCustomerID });
        }
    }
}
