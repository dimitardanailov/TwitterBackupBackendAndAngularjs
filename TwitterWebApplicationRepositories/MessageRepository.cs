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
        public MessageRepository() : base(new MessageDbContext())
        {
        }

        /// <summary>
        /// Filter last <paramref name="maximClientMessagesPerPage"/> user messages.
        /// </summary>
        /// <param name="userId">Which messages, we want to display.</param>
        /// <param name="page">Current page</param>
        /// <param name="orderDirection">Order direction can be "asc" or "desc"</param>
        /// <param name="maximClientMessagesPerPage">How many records show to be taked.</param>
        /// <returns></returns>
        public IEnumerable<ClientMessage> GetMessagesByUserIdPageAndOrderDirection(string userId, 
            int page, string orderDirection, int maximClientMessagesPerPage)
        {
            var messages = (from m in Context.Set<ClientMessage>()
                            where m.UserID.Equals(userId)
                            select new {
                                MessageID = m.MessageID,
                                Text = m.Text,
                                CreatedAt = m.CreatedAt
                            }).ToList()
                            .Select(x => new ClientMessage {
                                MessageID = x.MessageID,
                                Text = x.Text,
                                CreatedAt = x.CreatedAt,
                            });

            if (orderDirection.Equals("asc"))
            {
                messages = messages.OrderBy(m => m.CreatedAt);
            }
            else if (orderDirection.Equals("desc"))
            {
                messages = messages.OrderByDescending(m => m.CreatedAt);
            }

            return PaginateRecords(messages, page, maximClientMessagesPerPage);
        }
    }
}
