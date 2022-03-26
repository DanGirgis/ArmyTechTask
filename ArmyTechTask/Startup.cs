using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArmyTechTask.Startup))]
namespace ArmyTechTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
