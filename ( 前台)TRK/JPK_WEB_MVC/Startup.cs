using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JPK_WEB_MVC.Startup))]
namespace JPK_WEB_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
