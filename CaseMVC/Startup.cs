using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaseMVC.Startup))]
namespace CaseMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
