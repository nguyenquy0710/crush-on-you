using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoYqlp.WepApp.Startup))]
namespace CoYqlp.WepApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
