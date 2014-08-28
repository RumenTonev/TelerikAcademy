using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterLikeApp.Startup))]
namespace TwitterLikeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
