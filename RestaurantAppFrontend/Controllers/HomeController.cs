using Microsoft.AspNetCore.Mvc;
using RestaurantAppFrontend.Models;
using System.Diagnostics;

namespace RestaurantAppFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\Hjalm\\source\\repos\\RestaurantAppFrontend\\RestaurantAppFrontend\\wwwroot\\Resources\\info.txt");

            string fileContent = System.IO.File.ReadAllText(filePath);

            ViewData["FileContent"] = fileContent;
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
