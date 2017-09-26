using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracticeWeb.Startup))]
namespace PracticeWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
