﻿using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private SportsProContext context { get; set; }


        public CustomerController(SportsProContext ctx)
        {
            context = ctx;
        }
        //Add

        [HttpGet]
        public IActionResult Add()
        {
            CustomerViewModel viewModel = new CustomerViewModel(new Customer(), context.Countries.ToList());
            ViewBag.Action = "Add";
            return View("Edit", viewModel);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var customer = context.Customers.Find(id);
            CustomerViewModel viewModel = new CustomerViewModel(customer, context.Countries.ToList());

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel customerVM)
        {
            if (ModelState.IsValid)
            {
                if (customerVM.Customer.CustomerID == 0)
                {
                    context.Customers.Add(customerVM.Customer);
                    context.SaveChanges();
                    return RedirectToAction("List", "Customer");
                }
                else
                {
                    context.Customers.Update(customerVM.Customer);
                    context.SaveChanges();
                    return RedirectToAction("List", "Customer");
                }

            }

            else
            {
                ViewBag.Action = (customerVM.Customer.CustomerID == 0) ? "Add" : "Edit";
                CustomerViewModel viewModel = new CustomerViewModel(new Customer(), context.Countries.ToList());
                return View(viewModel);
            }
        }
        //List
        public IActionResult List()
        {
            var customers = context.Customers.ToList();
            return View(customers);
        }
        //Delete

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            ViewBag.Action = "Delete";
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("List", "Customer");
        }
    }
}
