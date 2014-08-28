using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieMVCDbDemo.Startup))]
namespace MovieMVCDbDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
