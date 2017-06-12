using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ventas.MVC.Startup))]
namespace Ventas.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
