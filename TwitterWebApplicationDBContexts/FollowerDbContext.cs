using System.Data.Entity;
using TwitterWebApplicationEntities;

namespace TwitterWebApplicationDBContexts
{
    public class FollowerDbContext : ApplicationDbContext
    {
        public DbSet<Follower> Followers { get; set; }
    }
}
