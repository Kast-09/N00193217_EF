using Microsoft.AspNetCore.Mvc;
using N00193217.Web.Models;
using System.Diagnostics;

namespace N00193217.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() {}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}