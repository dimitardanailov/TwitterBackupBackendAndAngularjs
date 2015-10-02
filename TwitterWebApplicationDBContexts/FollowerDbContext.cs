using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using TwitterWebApplication.Models;
using TwitterWebApplicationEntities;

namespace TwitterWebApplicationDBContexts
{
    public class FollowerDbContext : ApplicationDbContext
    {
        public DbSet<Follower> Followers { get; set; }
    }
}
