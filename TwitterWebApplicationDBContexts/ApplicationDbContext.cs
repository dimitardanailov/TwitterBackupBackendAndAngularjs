using Microsoft.AspNet.Identity.EntityFramework;
using TwitterWebApplicationEntities;

namespace TwitterWebApplicationDBContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(ClientConfigurations.DatabaseSettings.ConnectionString, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
