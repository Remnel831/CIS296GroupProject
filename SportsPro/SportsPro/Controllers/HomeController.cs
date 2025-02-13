using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ActiveTab = "Home";
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewBag.ActiveTab = "Home";
            return View();
        }

    }
}