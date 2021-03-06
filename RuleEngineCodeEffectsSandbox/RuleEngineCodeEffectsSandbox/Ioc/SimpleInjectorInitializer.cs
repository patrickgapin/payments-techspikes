﻿using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PinnacleSports.RuleRepo.Repository;
using PinnacleSports.RuleRepo.Repository.Interfaces;
using PinnacleSports.RuleService.Repository;
using PinnacleSports.RuleService.RuleServices;
using PinnacleSports.RuleService.RuleServices.Interfaces;
using PinnacleSports.Shared.RuleEngineFactory;
using PinnacleSports.Shared.RuleEngineFactory.Interfaces;
using RuleEngineCodeEffectsSandbox.Mapping;
using RuleEngineCodeEffectsSandbox.Mapping.Interfaces;
using RuleEngineCodeEffectsSandbox.RuleEngine;
using RuleEngineCodeEffectsSandbox.RuleEngine.Interfaces;
using RuleEngineCodeEffectsSandbox.Services;
using RuleEngineCodeEffectsSandbox.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using PinnacleSports.RuleService.Helpers;

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

            container.Register<ICreditCardRepository, CreditCardRepository>();
            container.Register<IDepositTransactionRepository, DepositTransactionRepository>();

            container.Register<ICreditCardDepositRepository, CreditCardDepositRepository>();
            container.Register<ICreditCardDepositMapping, CreditCardDepositMapping>();

            container.Register<IRuleEngineEvaluator, RuleEngineEvaluator>();
            
            var ruleEngineFactory = new RuleEngineFactory(container);
            ruleEngineFactory.Register<IDepositTransactionRepository, DepositTransactionRepository>(RuleEngineTypes.ImplementationType.DepositTransactionRepository);
            container.RegisterSingleton<IRuleEngineFactory>(ruleEngineFactory);


            HttpContextBase httpContextBase = new HttpContextWrapper(HttpContext.Current);
            container.RegisterPerWebRequest(() => httpContextBase);
        }
    }
}