using EventEase.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace EventEase.MVC.Controllers
{
    public class EventController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EventController> _logger;

        public EventController(IHttpClientFactory factory, ILogger<EventController> logger)
        {
            _httpClient = factory.CreateClient("EventEaseAPI"); // already registered in Program.cs
            _logger = logger;
        }

        // GET: /Event
        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm)
        {
            try
            {
                // ✅ keep your original API path
                var events = await _httpClient.GetFromJsonAsync<List<EventViewModel>>("api/event/sync")
                             ?? new List<EventViewModel>();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    events = events
                        .Where(e =>
                            (!string.IsNullOrEmpty(e.Title) && e.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrEmpty(e.Description) && e.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        )
                        .ToList();
                }

                ViewData["CurrentFilter"] = searchTerm; // keep the search box filled
                return View(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching events.");
                ViewData["CurrentFilter"] = searchTerm;
                return View(new List<EventViewModel>()); // return empty list on error
            }
        }

        // GET: /Event/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                // ✅ keep your original API path
                var ev = await _httpClient.GetFromJsonAsync<EventViewModel>($"api/event/sync/{id}");
                return ev is null ? NotFound() : View(ev);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching event details for Id {Id}", id);
                return NotFound();
            }
        }
    }
}
