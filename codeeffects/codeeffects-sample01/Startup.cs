using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(codeeffects_sample01.Startup))]
namespace codeeffects_sample01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
