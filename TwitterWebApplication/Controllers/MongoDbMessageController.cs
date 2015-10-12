using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwitterWebApplicationRepositories;
using TwitterWebApplicationRepositories.MongoDb.interfaces;
using TwitterWebApplicationEntities;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace TwitterWebApplication.Controllers
{
    [Authorize]
    public class MongoDbMessageController : ApiController
    {
        // private MongoDbMessageRepository repository = new MongoDbMessageRepository();
        private static readonly IMessageRepository _messages = new MongoDbMessageRepository();

        // POST api/MongoDbMessage
        [ResponseType(typeof(MongoDbMessage))]
        public async Task<IHttpActionResult> Post(MongoDbMessage newMessage)
        {
            string currentUserId = User.Identity.GetUserId();
            newMessage.UserID = currentUserId;

            if (newMessage.Text.Length < 10)
            {
                ModelState.AddModelError("", "Your message shoud be at least 10 characters");

                return this.BadRequest(this.ModelState);
            }

            // Post: http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/updating-related-data-with-the-entity-framework-in-an-asp-net-mvc-application
            try
            {
                MongoDbMessage message = _messages.AddMessage(newMessage);
                message.User = new ApplicationUser();
                message.User.Id = currentUserId;
                message.User.UserName = User.Identity.GetUserName();

                return this.Ok<MongoDbMessage>(message);
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
            catch (Exception)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

                return this.BadRequest(this.ModelState);
            }
        }
    }
}
