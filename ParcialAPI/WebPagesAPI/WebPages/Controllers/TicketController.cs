using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialAPI.DAL.Entities;
using System.Text.Json.Serialization;

namespace WebPages.Controllers
{
    public class TicketController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public TicketController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                //var url = "https://localhost:7258/api/Ticket/Get/";
                //var json = await _httpClient.CreateClient().GetStringAsync(url);
                //List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketById()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> GetTicketById(Guid? id)
        {
            try
            {
                var url = String.Format("https://localhost:7258/api/Ticket/Get/{0}", id);
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                return RedirectToAction("Edit", JsonConvert.DeserializeObject<Ticket>(json));
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Ticket ticket)
        {
            try
            {
                var url = String.Format("https://localhost:7258/api/Ticket/Edit/{0}", id);
            await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);
            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
    }
}
