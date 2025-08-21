using HostelManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace HostelManagement.MVC.Controllers
{
    public class RoomViewController : Controller
    {
        private readonly HttpClient _httpClient;

        public RoomViewController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HostelAPI");
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _httpClient.GetFromJsonAsync<List<RoomViewModel>>("Room");
            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _httpClient.GetFromJsonAsync<RoomViewModel>($"Room/{id}");
            if (room == null) return NotFound();
            return View(room);
        }
    }
}
