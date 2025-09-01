using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.MVC.Models;
using System.Net.Http.Json;

namespace ShopTrackPro.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserController> _logger;

        public UserController(IHttpClientFactory factory, ILogger<UserController> logger)
        {
            _httpClient = factory.CreateClient("ShopTrackProAPI");
            _logger = logger;
        }

        // GET: User/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<IEnumerable<UserViewModel>>("api/users");
                return View(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return View(new List<UserViewModel>());
            }
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<UserViewModel>($"api/users/{id}");
                if (user == null) return NotFound();

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching user details for Id={id}");
                return NotFound();
            }
        }
    }
}
