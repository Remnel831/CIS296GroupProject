using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class ProductController : Controller
	{
		private SportsProContext context { get; set; }


		public ProductController(SportsProContext ctx) 
		{
			context = ctx;
		}
		//Add

		[HttpGet]
		public IActionResult Add()
		{
            ViewBag.ActiveTab = "Product";
            ViewBag.Action = "Add";
			return View("Edit", new Product());
		}


		//Edit
		[HttpGet]
		public IActionResult Edit(int id)
		{
            ViewBag.ActiveTab = "Product";
            ViewBag.Action = "Edit";
			var product = context.Products.Find(id);
			return View(product);
		}

		[HttpPost]
		public IActionResult Edit(Product product) 
		{
            ViewBag.ActiveTab = "Product";
            if (ModelState.IsValid)
			{
				if (product.ProductID == 0) 
				{
					TempData["TempMessage"] = $"{product.Name} was added."; 
					context.Products.Add(product);
					context.SaveChanges();
					return RedirectToAction("List", "Product");
				}
				else
				{
                    TempData["TempMessage"] = $"{product.Name} was edited.";
                    context.Products.Update(product);
					context.SaveChanges();
					return RedirectToAction("List", "Product");
				}

			}

			else 
			{
				ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
				return View(product);
			}
		}

		//List
		[Route("products")]
		public IActionResult List() 
		{
            ViewBag.ActiveTab = "Product";
			ViewBag.Action = "Products";
            var products = context.Products.ToList();
			return View(products);
		}
		//Delete

		[HttpGet]
		public IActionResult Delete(int id) 
		{
            ViewBag.ActiveTab = "Product";
            var product = context.Products.Find(id);
			TempData["ProductName"] = product.Name;
            return View(product);
		}

		[HttpPost]
		public IActionResult Delete(Product product)
		{
			string name = product.Name;
            TempData["TempMessage"] = $"{TempData["ProductName"]?.ToString()} was deleted.";
            ViewBag.Action = "Delete";
            context.Products.Remove(product);
			context.SaveChanges();
			return RedirectToAction("List", "Product");
		}
	}
}
