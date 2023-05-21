using Microsoft.EntityFrameworkCore;
using ParcialAPI.DAL.Entities;

namespace ParcialAPI.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }

    }
}
