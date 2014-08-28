using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCAjaxDemo.Startup))]
namespace MVCAjaxDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
