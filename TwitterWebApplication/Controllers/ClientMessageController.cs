using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            clientMessage.UserID = User.Identity.GetUserId();
            this.repository.Insert(clientMessage);
            
            return this.Ok<bool>(true);
        }
    }
}
