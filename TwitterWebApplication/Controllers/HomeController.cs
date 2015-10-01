using ClientConfigurations;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Web.Mvc;
using TweetSharp;

namespace TwitterWebApplication.Controllers
{
    // [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FavouriteList()
        {
            return View();
        }

        public ActionResult Authorize()
        {
            // Step 1 - Retrieve an OAuth Request Token
            TwitterService service = new TwitterService(TwitterSettings.ConsumerKey, TwitterSettings.ConsumerSecret);

            // This is the registered callback URL
            OAuthRequestToken requestToken = service.GetRequestToken("http://dimitar-dev/Home/AuthorizeCallback");

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);

            return new RedirectResult(uri.ToString(), false /*permanent*/);
        }

        // This URL is registered as the application's callback at http://dev.twitter.com
        public ActionResult AuthorizeCallback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            // Step 3 - Exchange the Request Token for an Access Token
            TwitterService service = new TwitterService(TwitterSettings.ConsumerKey, TwitterSettings.ConsumerSecret);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

            // Step 4 - User authenticates using the Access Token
            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());
            ViewBag.Title = string.Format("Your username is {0}", user.ScreenName);

            return View();
        }
    }
}