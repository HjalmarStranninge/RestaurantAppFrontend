using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantAppFrontend.Models;
using RestaurantAppFrontend.RestaurantAppFrontend;
using System.Text;

namespace RestaurantAppFrontend.Controllers
{
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;
        private string _baseUri = "https://localhost:7185/";

        public BookingController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_baseUri}getallbookings");
            var json = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
            return View(bookings);
        }

        public async Task<IActionResult> BookingAdmin()
        {
            ViewData["Title"] = "Admin - Bookings";
            var response = await _httpClient.GetAsync($"{_baseUri}getallbookings");
            var json = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUri}deletebooking?id={id}");
            return RedirectToAction("BookingAdmin");
        }

        [HttpGet]
        public IActionResult AddBookingForm()
        {
            ViewBag.Action = "AddBooking";
            return PartialView("AddBooking", new CreateBooking());
        }
        [HttpGet]
        public async Task<IActionResult> EditBookingForm(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUri}getbooking?id={id}");
            var json = await response.Content.ReadAsStringAsync();
            var booking = JsonConvert.DeserializeObject<Booking>(json);
            ViewBag.Action = "UpdateBooking";
            return PartialView("UpdateBooking", booking);
        }


        [HttpPost]
        public async Task<IActionResult> AddBooking(CreateBooking booking)
        {
            var json = JsonConvert.SerializeObject(booking);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUri}makenewbooking", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingAdmin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return RedirectToAction("BookingAdmin");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(Booking booking)
        {
            var json = JsonConvert.SerializeObject(booking);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUri}updatebooking", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingAdmin");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Server error: {errorContent}");
                return RedirectToAction("BookingAdmin");
            }
        }
    }
}
