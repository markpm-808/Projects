using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarDealership.Startup))]
namespace CarDealership
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
