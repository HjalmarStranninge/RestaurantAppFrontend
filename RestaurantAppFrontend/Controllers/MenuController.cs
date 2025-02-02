using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantAppFrontend.Models;
using RestaurantAppFrontend.RestaurantAppFrontend;
using System.Text;

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

        [ServiceFilter(typeof(AdminAuthorizationFilter))]
        public async Task<IActionResult> MenuAdmin()
        {
            var response = await _httpClient.GetAsync($"{_baseUri}getallmenuitems");
            var json = await response.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(json);
            return View(menuItems);
        }

        [ServiceFilter(typeof(AdminAuthorizationFilter))]
        [HttpPost]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUri}deletemenuitem?id={id}");
            return RedirectToAction("MenuAdmin");
        }

        [ServiceFilter(typeof(AdminAuthorizationFilter))]
        [HttpGet]
        public IActionResult AddMenuItemForm()
        {
            ViewBag.Action = "AddMenuItem";
            return PartialView("AddMenuItem", new MenuItem());
        }

        [ServiceFilter(typeof(AdminAuthorizationFilter))]
        [HttpGet]
        public async Task<IActionResult> EditMenuItemForm(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUri}getmenuitem?id={id}");
            var json = await response.Content.ReadAsStringAsync();
            var menuItem = JsonConvert.DeserializeObject<MenuItem>(json);
            ViewBag.Action = "UpdateMenuItem";
            return PartialView("UpdateMenuItem", menuItem);
        }

        [ServiceFilter(typeof(AdminAuthorizationFilter))]
        [HttpPost]
        public async Task<IActionResult> AddMenuItem(MenuItem menuItem)
        {

            var json = JsonConvert.SerializeObject(menuItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUri}createmenuitem", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MenuAdmin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return RedirectToAction("MenuAdmin");
            }

        }

        [ServiceFilter(typeof(AdminAuthorizationFilter))]
        [HttpPost]
        public async Task<IActionResult> UpdateMenuItem(MenuItem menuItem)
        {

            var json = JsonConvert.SerializeObject(menuItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUri}updatemenuitem", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MenuAdmin");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Server error: {errorContent}");
                return RedirectToAction("MenuAdmin");
            }

        }
    }

}
