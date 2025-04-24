using Microsoft.AspNetCore.Mvc;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Controllers
{
	public class ProductController : Controller
	{
		private Repository<Product> Products { get; set; }


        public ProductController(SportsProContext ctx) 
		{
            Products = new Repository<Product>(ctx);
		}
		//Add

		[HttpGet]
		public ViewResult Add()
		{
            ViewBag.ActiveTab = "Product";
            ViewBag.Action = "Add";
			return View("Edit", new Product());
		}


		//Edit
		[HttpGet]
		public ViewResult Edit(int id)
		{
            ViewBag.ActiveTab = "Product";
            ViewBag.Action = "Edit";
			var product = Products.Get(id);
			return View(product);
		}

		[HttpPost]
		public RedirectToActionResult Edit(Product product) 
		{
            ViewBag.ActiveTab = "Product";
            if (ModelState.IsValid)
			{
				if (product.ProductID == 0) 
				{
					TempData["TempMessage"] = $"{product.Name} was added."; 
					Products.Insert(product);
					Products.Save();
					return RedirectToAction("List", "Product");
				}
				else
				{
                    TempData["TempMessage"] = $"{product.Name} was edited.";
                    Products.Update(product);
					Products.Save();
					return RedirectToAction("List", "Product");
				}

			}
			else 
			{
				return RedirectToAction("EditError", "Product");
			}
		}

		[HttpPost]
		public ViewResult EditError(Product product)
		{
            ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
            return View(product);
        }

		//List
		[Route("products")]
		public ViewResult List() 
		{
            ViewBag.ActiveTab = "Product";
			      ViewBag.Action = "Products";
            var products = Products.List(new QueryOptions<Product>());
			return View(products);
		}
		//Delete

		[HttpGet]
		public ViewResult Delete(int id) 
		{
            ViewBag.ActiveTab = "Product";
            var product = Products.Get(id);
			TempData["ProductName"] = product.Name;
            return View(product);
		}

		[HttpPost]
		public RedirectToActionResult Delete(Product product)
		{
			string name = product.Name;
            TempData["TempMessage"] = $"{TempData["ProductName"]?.ToString()} was deleted.";
            ViewBag.Action = "Delete";
            Products.Delete(product);
			Products.Save();
			return RedirectToAction("List", "Product");
		}
	}
}
