using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantAppFrontend.Models;

namespace RestaurantAppFrontend.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
