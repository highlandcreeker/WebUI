using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FBPortal.WebUI.Startup))]
namespace FBPortal.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
