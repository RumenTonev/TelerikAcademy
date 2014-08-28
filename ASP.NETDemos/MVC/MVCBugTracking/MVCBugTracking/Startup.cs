using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCBugTracking.Startup))]
namespace MVCBugTracking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
