
using PinnacleSports.RuleService.Helpers;
using SimpleInjector;

namespace RuleEngineCodeEffectsSandbox.Ioc
{
    public static class RuleInjectorInitializer
    {
        private static Container container;
        public static void Initialize()
        {
            container = new Container();

            container.Register<IClientHelper, NorthAmericaClientHelper>();
            //container.Register<IClientHelper, AfricaClientHelper>();

            container.Verify();
        }

        public static Container Container { get { return container; } }
    }
}