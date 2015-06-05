using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebRole.Startup))]
namespace MyWebRole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
