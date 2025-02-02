using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantAppFrontend.Models;
using RestaurantAppFrontend.RestaurantAppFrontend;

namespace RestaurantAppFrontend.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Other admin actions
    }

}
