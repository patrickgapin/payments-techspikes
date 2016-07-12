using System.Reflection;
using System.Web.Mvc;
using InRuleSandbox.Factories;
using InRuleSandbox.Factories.Interfaces;
using InRuleSandbox.Ioc;
using InRuleSandbox.Models;
using InRuleSandbox.Services;
using InRuleSandbox.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]

namespace InRuleSandbox.Ioc
{
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IInRuleService<Customer>, InRuleService<Customer>>();
            container.Register<IInRuleService<Transaction>, InRuleService<Transaction>>();
            container.Register<IInRuleFactory, InRuleFactory>();
        }
    }
}