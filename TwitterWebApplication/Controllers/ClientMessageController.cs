using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TwitterWebApplicationDBContexts;
using TwitterWebApplicationEntities;
using TwitterWebApplicationRepositories;

namespace TwitterWebApplication.Controllers
{
    [Authorize]
    public class ClientMessageController : ApplicationApiController<ClientMessage>
    {
        private const int MaximClientMessagesPerPage = 50;

        public ClientMessageController() : base(new MessageRepository())
        {
            
        }

        // POST api/ClientMessage
        [ResponseType(typeof(ClientMessage))]
        public async Task<IHttpActionResult> Post(ClientMessage clientMessage)
        {
            string currentUserId = User.Identity.GetUserId();
            clientMessage.UserID = currentUserId;

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Post: http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/updating-related-data-with-the-entity-framework-in-an-asp-net-mvc-application
            try
            {
                this.repository.Insert(clientMessage);

                // Update User information.
                clientMessage.User = new ApplicationUser();
                clientMessage.User.Id = currentUserId;
                clientMessage.User.UserName = User.Identity.GetUserName();

                return this.Ok<ClientMessage>(clientMessage);
            }
            catch (DbUpdateException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return this.BadRequest(this.ModelState);
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return this.BadRequest(this.ModelState);
            }
        }

        // GET api/ClientMessage
        public IEnumerable<ClientMessage> Get()
        {
            string currentUserId = User.Identity.GetUserId();
            int page = 1;

            var messageRepository = this.repository as MessageRepository;

            IEnumerable<ClientMessage> messages = messageRepository
                .GetMessagesByUserIdPageAndOrderDirection(currentUserId, page, "desc", MaximClientMessagesPerPage);

            return messages;
        }
    }
}
