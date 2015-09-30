using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TwitterWebApplication.Models;
using Microsoft.Owin.Security.Twitter;

namespace TwitterWebApplication
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            var twitterOptions = GetTwitterConfigurations();
            app.UseTwitterAuthentication(twitterOptions);
        }

        /// <summary>
        /// Current Configuration Update all necessary settings for Twitter.
        /// Articles: 
        /// http://www.oauthforaspnet.com/providers/twitter/
        /// http://www.asp.net/mvc/overview/security/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on
        /// 
        /// 
        /// Twitter credentials should be stored in: <see cref="Security/AppSettingsSecrets.config"/>.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private TwitterAuthenticationOptions GetTwitterConfigurations()
        {
            string TwitterCallbackPath = ClientConfigurations.AppSettings.Setting<string>("TwitterCallbackURL");

            var options = new TwitterAuthenticationOptions
            {
                ConsumerKey = "NSrnH0U6KQnZu06uCkXP9cRbF",
                ConsumerSecret = "LnkPBAL2PJYU1FXFTyVik7ua3nqJnQ9LcC6zw2xDPhBUpaoD7u",
                BackchannelCertificateValidator = null,
                Provider = new TwitterAuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        // These are then added as claims to the ClaimsIdentity which is available as Identity property of the context variable.
                        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:tokens:twitter:accesstoken", context.AccessToken));
                        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:tokens:twitter:accesstokensecret", context.AccessTokenSecret));
                    }
                }
            };

            return options;
        }
    }
}