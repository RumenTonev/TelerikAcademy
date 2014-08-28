using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RSSReader.Startup))]
namespace RSSReader
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
