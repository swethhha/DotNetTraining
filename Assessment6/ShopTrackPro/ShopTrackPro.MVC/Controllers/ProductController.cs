using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.MVC.Models;
using System.Net.Http.Json;

namespace ShopTrackPro.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IHttpClientFactory factory, ILogger<ProductController> logger)
        {
            _httpClient = factory.CreateClient("ShopTrackProAPI");
            _logger = logger;
        }

        // GET: Product/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("api/products");
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products");
                return View(new List<ProductViewModel>());
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var product = await _httpClient.GetFromJsonAsync<ProductViewModel>($"api/products/{id}");
                if (product == null) return NotFound();

                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching product details for Id={id}");
                return NotFound();
            }
        }
    }
}
