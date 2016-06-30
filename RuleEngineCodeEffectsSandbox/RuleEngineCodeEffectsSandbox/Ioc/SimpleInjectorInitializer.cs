using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RuleEngineCodeEffectsSandbox.Services;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace RuleEngineCodeEffectsSandbox.Ioc
{
    public static class SimpleInjectorInitializer
    {

        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<IRuleService, RuleService>();

            HttpContextBase httpContextBase = new HttpContextWrapper(HttpContext.Current);
            container.RegisterPerWebRequest(() => httpContextBase);
            
        }
    }
}