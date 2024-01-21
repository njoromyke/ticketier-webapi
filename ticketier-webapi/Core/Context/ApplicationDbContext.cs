using Microsoft.EntityFrameworkCore;

namespace ticketier_webapi.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Entities.Ticket> Tickets { get; set; }
    }
}
