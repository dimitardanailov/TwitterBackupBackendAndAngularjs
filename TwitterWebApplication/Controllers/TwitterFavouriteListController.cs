using System.Web.Http;
using System.Security.Claims;
using System.Collections.Generic;
using TwitterApplicationLibrary;
using TweetSharp;
using TwitterWebApplicationDBContexts;

namespace TwitterWebApplication.Controllers
{
    [Authorize]
    public class TwitterFavouriteListController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        public IEnumerable<TwitterStatus> GetTwitters()
        {
            var identity = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            ApplicationTwitterService service = ApplicationTwitterService.InitializeServiceConnection(claims);
            var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());

            return tweets;
        }
    }
}
