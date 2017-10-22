using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VienShops.Startup))]
namespace VienShops
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
