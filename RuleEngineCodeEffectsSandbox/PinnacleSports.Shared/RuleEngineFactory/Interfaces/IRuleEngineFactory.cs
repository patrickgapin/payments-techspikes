using SimpleInjector;

namespace PinnacleSports.Shared.RuleEngineFactory.Interfaces
{
    public interface IRuleEngineFactory
    {
        TInterface CreateNew<TInterface>(RuleEngineTypes.ImplementationType implementationType);

        void Register<TInterface, TImplementation>(RuleEngineTypes.ImplementationType implementationType,
                Lifestyle lifestyle = null)
            where TImplementation : class, TInterface
            where TInterface : class;
    }
}