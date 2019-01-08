using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikesShop.Startup))]
namespace BikesShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
