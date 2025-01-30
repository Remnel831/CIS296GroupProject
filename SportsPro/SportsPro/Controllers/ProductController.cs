using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class ProductController : Controller
	{
		//Add

		[HttpGet]
		public IActionResult Add()
		{
			//
			return RedirectToAction("List", "Product");
		}


		//Edit
		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewBag.Action = "Edit";
			// var product = context.Products.Find(id);
			return View();
		}

		[HttpPost]
		public IActionResult Edit(Product product) 
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("List", "Product");
			}

			else 
			{
				return View();
			}
		}
		//List
		public IActionResult List() 
		{
			//var products = 
			return View();
		}
	}
}
