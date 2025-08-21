using HostelManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace HostelManagement.MVC.Controllers
{
    public class StudentViewController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentViewController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HostelAPI");
        }

        public async Task<IActionResult> Index()
        {
            var students = await _httpClient.GetFromJsonAsync<List<StudentViewModel>>("Student");
            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<StudentViewModel>($"Student/{id}");
            if (student == null) return NotFound();
            return View(student);
        }
    }
}
