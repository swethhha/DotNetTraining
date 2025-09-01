using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.MVC.Models;
using System.Net.Http.Json;

namespace ShopTrackPro.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IHttpClientFactory factory, ILogger<OrderController> logger)
        {
            _httpClient = factory.CreateClient("ShopTrackProAPI");
            _logger = logger;
        }

        // GET: Order/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await _httpClient.GetFromJsonAsync<IEnumerable<OrderViewModel>>("api/orders");
                return View(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders");
                return View(new List<OrderViewModel>());
            }
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var order = await _httpClient.GetFromJsonAsync<OrderViewModel>($"api/orders/{id}");
                if (order == null) return NotFound();

                return View(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching order details for Id={id}");
                return NotFound();
            }
        }
    }
}
