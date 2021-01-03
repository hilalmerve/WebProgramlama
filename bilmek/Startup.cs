using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bilmek.Startup))]
namespace bilmek
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
