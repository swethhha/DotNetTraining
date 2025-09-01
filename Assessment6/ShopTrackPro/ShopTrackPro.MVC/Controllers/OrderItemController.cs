using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.MVC.Models;
using System.Net.Http.Json;

namespace ShopTrackPro.MVC.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderItemController> _logger;

        public OrderItemController(IHttpClientFactory factory, ILogger<OrderItemController> logger)
        {
            _httpClient = factory.CreateClient("ShopTrackProAPI");
            _logger = logger;
        }

        // GET: OrderItem/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var items = await _httpClient.GetFromJsonAsync<IEnumerable<OrderItemViewModel>>("api/orderitems");
                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order items");
                return View(new List<OrderItemViewModel>());
            }
        }

        // GET: OrderItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var item = await _httpClient.GetFromJsonAsync<OrderItemViewModel>($"api/orderitems/{id}");
                if (item == null) return NotFound();
                return View(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching order item details for Id={id}");
                return NotFound();
            }
        }
    }
}
