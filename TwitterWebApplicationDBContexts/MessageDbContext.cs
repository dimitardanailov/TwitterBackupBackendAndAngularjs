using System.Data.Entity;
using TwitterWebApplicationEntities;

namespace TwitterWebApplicationDBContexts
{
    public class MessageDbContext : ApplicationDbContext
    {
        public DbSet<ClientMessage> Messages { get; set; }
    }
}
