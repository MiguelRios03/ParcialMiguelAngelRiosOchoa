using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialAPI.DAL;
using ParcialAPI.DAL.Entities;
using System.Data.Common;

namespace ParcialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private static readonly string[] Entrances = new[]
        {
        "Norte", "Sur", "Oriental", "Occidental"
        };

        private readonly DataBaseContext _context;

        public TicketsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();

            if (tickets == null) return NotFound("No hay tickets vendidos");

            return tickets;
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]

        public async Task<ActionResult<Ticket>> EditTicket(Guid? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.ID == id);

            if (ticket == null) return NotFound("Boleta no válida");

            if (ticket.IsUsed == true) return Ok("Boleta ya usada");

            ticket.UseDate = DateTime.Now;
            ticket.IsUsed = true;
            ticket.EntranceGate = Entrances[Random.Shared.Next(Entrances.Length)];

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            return Ok(String.Format("Boleta válida, puede ingresar al concierto"));
        } 
        
    }


}
