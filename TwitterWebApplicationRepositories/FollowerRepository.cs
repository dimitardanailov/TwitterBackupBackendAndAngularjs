using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterWebApplicationDBContexts;
using TwitterWebApplicationEntities;

namespace TwitterWebApplicationRepositories
{
    public class FollowerRepository : Repository<Follower>
    {
        public FollowerRepository() : base(new FollowerDbContext())
        {
        }

    }
}
