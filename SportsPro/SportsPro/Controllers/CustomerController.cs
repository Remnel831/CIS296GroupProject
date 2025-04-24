using Microsoft.AspNetCore.Mvc;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private Repository<Country> Countries { get; set; }
        private Repository<Customer> Customers { get; set; }


        public CustomerController(SportsProContext ctx)
        {
            Countries = new Repository<Country>(ctx);
            Customers = new Repository<Customer>(ctx);
        }
        //Add

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActiveTab = "Customer";
            CustomerViewModel viewModel = new CustomerViewModel(new Customer(), Countries.List(new QueryOptions<Country>()));
            ViewBag.Action = "Add";
            return View("Edit", viewModel);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveTab = "Customer";
            ViewBag.Action = "Edit";
            var customer = Customers.Get(id);
            CustomerViewModel viewModel = new CustomerViewModel(customer, Countries.List(new QueryOptions<Country>()));

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel customerVM)
        {
            ViewBag.ActiveTab = "Customer";
            if (ModelState.IsValid)
            {
                if (customerVM.Customer.CustomerID == 0)
                {
                    Customers.Insert(customerVM.Customer);
                    Customers.Save();
                    return RedirectToAction("List", "Customer");
                }
                else
                {
                    Customers.Update(customerVM.Customer);
                    Customers.Save();
                    return RedirectToAction("List", "Customer");
                }

            }

            else
            {
                ViewBag.Action = (customerVM.Customer.CustomerID == 0) ? "Add" : "Edit";
                CustomerViewModel viewModel = new CustomerViewModel(new Customer(), Countries.List(new QueryOptions<Country>()));
                return View(viewModel);
            }
        }
        //List
        [Route("customers")]
        public IActionResult List()
        {
            ViewBag.ActiveTab = "Customer";
            ViewBag.Action = "Customers";

            var customers = Customers.List(new QueryOptions<Customer>());
            return View(customers);
        }
        //Delete

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.ActiveTab = "Customer";
            var customer = Customers.Get(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            ViewBag.Action = "Delete";
            Customers.Delete(customer);
            Customers.Save();
            return RedirectToAction("List", "Customer");
        }
    }
}
