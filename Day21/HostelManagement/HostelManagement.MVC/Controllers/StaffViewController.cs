using HostelManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace HostelManagement.MVC.Controllers
{
    public class StaffViewController : Controller
    {
        private readonly HttpClient _httpClient;

        public StaffViewController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HostelAPI");
        }

        public async Task<IActionResult> Index()
        {
            var staffList = await _httpClient.GetFromJsonAsync<List<StaffViewModel>>("Staff");
            return View(staffList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var staff = await _httpClient.GetFromJsonAsync<StaffViewModel>($"Staff/{id}");
            if (staff == null) return NotFound();
            return View(staff);
        }
    }
}
