namespace ParcialAPI.DAL
{
    public class SeederDB
    {
        private readonly DataBaseContext _context;
        public SeederDB(DataBaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateTicketsAsync();
        }
        private async Task PopulateTicketsAsync()
        {
           
            if (!_context.Tickets.Any())
            {

                for (int i = 0; i < 50000; i++)
                {
                    _context.Tickets.Add(new Entities.Ticket { ID = Guid.NewGuid(), UseDate = null, IsUsed = false, EntranceGate = null });
                }
                
            }
            await _context.SaveChangesAsync();
        }
    }
}
