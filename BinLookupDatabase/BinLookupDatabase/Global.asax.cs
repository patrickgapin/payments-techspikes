using System.Web;
using System.Web.Http;
using BinLookupDatabase.Repository;
using BinLookupDatabase.Repository.Interface;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Container = SimpleInjector.Container;

namespace BinLookupDatabase
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<IUserRepository, UserRepository>();
        }
    }
}

