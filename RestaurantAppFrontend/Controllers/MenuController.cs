using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantAppFrontend.Models;

namespace RestaurantAppFrontend.Controllers
{
    public class MenuController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _baseUri = "https://localhost:7185/";

        public MenuController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_baseUri}getallmenuitems");

            var json = await response.Content.ReadAsStringAsync();

            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);

            return View(menuItems);
        }
    }
}
