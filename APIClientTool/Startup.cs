using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APIClientTool.Startup))]
namespace APIClientTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
