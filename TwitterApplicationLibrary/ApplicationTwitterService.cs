using ClientConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterApplicationLibrary
{
    public class ApplicationTwitterService : TwitterService
    {
        public ApplicationTwitterService() : base(TwitterSettings.ConsumerKey, TwitterSettings.ConsumerSecret)
        {
        }

        /// <summary>
        /// Method try using <see cref="TwitterService"/> to make authentication.
        /// If you everything is fine, we can access user information.
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static ApplicationTwitterService InitializeServiceConnection(IEnumerable<Claim> claims)
        {
            var service = new ApplicationTwitterService();

            // Retrieve the twitter access token and claim
            var accessTokenClaim = claims.FirstOrDefault(x => x.Type == TwitterSettings.AccessTokenClaimType);
            var accessTokenSecretClaim = claims.FirstOrDefault(x => x.Type == TwitterSettings.AccessTokenSecretClaimType);

            if (accessTokenClaim == null || accessTokenSecretClaim == null)
            {
                throw new Exception(string.Format("Invalid accessTokenClaim or accessTokenSecretClaim"));
            }

            service.AuthenticateWith(accessTokenClaim.Value, accessTokenSecretClaim.Value);

            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());

            if (user == null)
            {
                throw new Exception(string.Format("Application can't VerifyCredentials"));
            }

            return service;
        }
    }
}
