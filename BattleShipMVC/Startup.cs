using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BattleShipMVC.Startup))]
namespace BattleShipMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
