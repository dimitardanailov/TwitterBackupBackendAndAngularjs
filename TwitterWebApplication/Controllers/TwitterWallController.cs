using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using TweetSharp;
using TwitterApplicationLibrary;

namespace TwitterWebApplication.Controllers
{
    [Authorize]
    public class TwitterWallController : ApiController
    {
        public class TwitterWallMessage
        {
            public long TweetID { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }
            public string ProfileImage { get; set; }
            public string Text { get; set; }
            public DateTime Created_at { get; set; }

            public TwitterWallMessage(TwitterStatus tweet)
            {
                TweetID = tweet.Id;
                Name = tweet.User.Name;
                Username = tweet.User.ScreenName;
                ProfileImage = tweet.User.ProfileImageUrl;
                Text = tweet.Text;
                Created_at = tweet.CreatedDate;
            }
        }

        /// <summary>
        /// Get user twitter wall.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TwitterWallMessage> GetTweets()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            ApplicationTwitterService service = ApplicationTwitterService.InitializeServiceConnection(claims);
            var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
            var twitterWallTweets = new List<TwitterWallMessage>();

            foreach (var tweet in tweets)
            {
                twitterWallTweets.Add(new TwitterWallMessage(tweet));
            }

            return twitterWallTweets;
        }
    }
}
