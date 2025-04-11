using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EHotelServices.Startup))]
namespace EHotelServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
