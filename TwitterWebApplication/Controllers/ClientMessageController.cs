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
using TwitterWebApplicationEntities;
using TwitterWebApplicationRepositories;

namespace TwitterWebApplication.Controllers
{
    [Authorize]
    public class ClientMessageController : ApplicationApiController<ClientMessage>
    {
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

                return this.Ok<bool>(true);
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
    }
}
