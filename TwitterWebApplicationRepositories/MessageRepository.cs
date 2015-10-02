using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using TwitterWebApplicationDBContexts;
using TwitterWebApplicationEntities;

namespace TwitterWebApplicationRepositories
{
    public class MessageRepository : Repository<ClientMessage>
    {
        protected IDbSet<ClientMessage> DbSet { get; set; }
        protected MessageDbContext MessageContext { get; set; }

        public MessageRepository(MessageDbContext context) : base(context)
        {
            MessageContext = context;
        }
    }
}
