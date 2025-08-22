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
            _httpClient = factory.CreateClient("EventEaseAPI"); // Register in Program.cs
            _logger = logger;
        }

        // GET: Event/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var events = await _httpClient.GetFromJsonAsync<IEnumerable<EventViewModel>>("api/event/sync");
                return View(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching events");
                return View(new List<EventViewModel>());
            }
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var ev = await _httpClient.GetFromJsonAsync<EventViewModel>($"api/event/sync/{id}");
                if (ev == null)
                    return NotFound();

                return View(ev);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching event details for Id={id}");
                return NotFound();
            }
        }
    }
}
