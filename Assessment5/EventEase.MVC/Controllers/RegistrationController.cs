using EventEase.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace EventEase.MVC.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IHttpClientFactory factory, ILogger<RegistrationController> logger)
        {
            _httpClient = factory.CreateClient("EventEaseAPI");
            _logger = logger;
        }

        // GET: Registration/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var registrations = await _httpClient.GetFromJsonAsync<IEnumerable<RegistrationViewModel>>("api/registration/sync");
                return View(registrations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching registrations");
                return View(new List<RegistrationViewModel>());
            }
        }

        // GET: Registration/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var registration = await _httpClient.GetFromJsonAsync<RegistrationViewModel>($"api/registration/sync/{id}");
                if (registration == null)
                    return NotFound();

                return View(registration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching registration details for Id={id}");
                return NotFound();
            }
        }
    }
}
