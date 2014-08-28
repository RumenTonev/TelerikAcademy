using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HWSessionState.Startup))]
namespace HWSessionState
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
