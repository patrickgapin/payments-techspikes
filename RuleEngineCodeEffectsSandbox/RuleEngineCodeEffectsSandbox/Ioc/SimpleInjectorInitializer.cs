using System.Reflection;
using System.Web;
using System.Web.Mvc;
using PinnacleSports.RuleRepo.Repository;
using PinnacleSports.RuleRepo.Repository.Interfaces;
using PinnacleSports.RuleService.Repository;
using PinnacleSports.RuleService.RuleServices;
using PinnacleSports.RuleService.RuleServices.Interfaces;
using RuleEngineCodeEffectsSandbox.Mapping;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;
using RuleEngineCodeEffectsSandbox.Services;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace RuleEngineCodeEffectsSandbox.Ioc
{
    public static class SimpleInjectorInitializer
    {

        public static void Initialize()
        {
            var container = new Container();
            //container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            //GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<IRuleService, RuleService>();

            container.Register<ICustomerRuleService, CustomerRuleService>();
            container.Register<ICreditCardRuleService, CreditCardRuleService>();
            container.Register<ICreditCardService, CreditCardService>();

            container.Register<IDepositTransactionRepository, DepositTransactionRepository>();

            container.Register<ICreditCardDepositRepository, CreditCardDepositRepository>();
            container.Register<ICreditCardDepositMapping, CreditCardDepositMapping>();

            HttpContextBase httpContextBase = new HttpContextWrapper(HttpContext.Current);
            container.RegisterPerWebRequest(() => httpContextBase);
        }
    }
}