using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialAPI.DAL.Entities;
using System.Text.Json.Serialization;

namespace WebPages.Controllers
{
    public class TicketController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public TicketController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var url = String.Format("https://localhost:7258/api/Tickets/Edit/{0}", id);
            await _httpClient.CreateClient().PutAsJsonAsync(url, id);
            return View("Index");
        }
    }
}
