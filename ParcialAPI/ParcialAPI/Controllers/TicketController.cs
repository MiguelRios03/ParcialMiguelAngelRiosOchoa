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
    public class TicketController : ControllerBase
    {
        private static readonly string[] Entrances = new[]
        {
        "Norte", "Sur", "Oriental", "Occidental"
        };

        private readonly DataBaseContext _context;

        public TicketController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();

            if (tickets == null) return NotFound();

            return tickets;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/id")]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(c => c.ID == id);

            if (ticket == null) return NotFound("La boleta no es válida.");

            if (ticket.IsUsed == true) return BadRequest("La boleta ya fue usada.");

            if (ticket.IsUsed == false && ticket != null) return Ok("La boleta es valida, puede ingresar al concierto.");

            return Ok(ticket);
        }

        [HttpPost, ActionName("Edit")]
        [Route("Edit")]

        public async Task<ActionResult<Ticket>> EditTicket(Guid? id, Ticket ticket)
        {
            try
            {
                ticket.UseDate = DateTime.Now;
                ticket.IsUsed = true;
                ticket.EntranceGate = Entrances[Random.Shared.Next(Entrances.Length)];

                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        } 
        
    }


}
