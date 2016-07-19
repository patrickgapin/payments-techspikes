using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AngularJsWithTypeScript.Startup))]
namespace AngularJsWithTypeScript
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);


            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}