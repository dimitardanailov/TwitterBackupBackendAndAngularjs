using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterWebApplication.Startup))]
namespace TwitterWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
