using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using RestaurantAppFrontend.Models;
using System.Diagnostics;


namespace RestaurantAppFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private string _baseUri = "https://localhost:7185/";


        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            const string hardcodedUsername = "admin";
            const string hardcodedPassword = "password";

            if (username == hardcodedUsername && password == hardcodedPassword)
            {
                HttpContext.Session.SetString("isAdmin", "true");
                _logger.LogInformation("Admin session set.");
                return Json(new { success = true });
            }
            else
            {
                _logger.LogWarning("Invalid login attempt.");
                return Json(new { success = false });
            }
        }

        public async Task<IActionResult> Index()
        {
            // Fetching a long string from a txt file and putting it in a ViewData
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "C:\\Users\\Hjalm\\source\\repos\\RestaurantAppFrontend\\RestaurantAppFrontend\\wwwroot\\Resources\\info.txt");
            string fileContent = System.IO.File.ReadAllText(filePath);
            ViewData["FileContent"] = fileContent;

            var response = await _httpClient.GetAsync($"{_baseUri}getallmenuitems");

            var json = await response.Content.ReadAsStringAsync();

            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);

            return View(menuItems);
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
