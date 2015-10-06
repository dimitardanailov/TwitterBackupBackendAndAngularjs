using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TweetSharp;
using TwitterApplicationLibrary;
using TwitterWebApplicationEntities;
using TwitterWebApplicationRepositories;

namespace TwitterWebApplication.Controllers
{
    [Authorize]
    public class TweetController : ApplicationApiController<ClientMessage>
    {
        public TweetController() : base(new MessageRepository())
        {

        }

        [ResponseType(typeof(TwitterStatus))]
        public async Task<IHttpActionResult> Post(ClientMessage clientMessage)
        {
            // Get user claims. Claims are stored in database.
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            ApplicationTwitterService service = ApplicationTwitterService.InitializeServiceConnection(claims);

            // Create a new tweet.
            try
            {
                SendTweetOptions options = new SendTweetOptions();
                // SendTweetOptions.Status is required field.
                options.Status = clientMessage.Text;

                var twitterStatus = service.SendTweet(options);

                // If you have any problem. twitterStatus will be equal to null.
                if (twitterStatus != null)
                {
                    return this.Ok<TwitterStatus>(twitterStatus);
                }
                else
                {
                    // Now we try to extract what is problem.
                    try
                    {
                        TwitterError error = service.Deserialize<TwitterError>(service.Response.Response);

                        if (error != null)
                        {
                            return this.BadRequest(error.Message);
                        }
                        else
                        {
                            return this.BadRequest("Please try again.");
                        }
                    }
                    catch (Exception exception)
                    {
                        return this.BadRequest(exception.Message);
                    }
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(exception.Message);
            }
        }
    }
}
