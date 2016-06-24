using SimpleInjector;

namespace NEventStore.Common.Ioc.Interfaces
{
    public interface IRequestHandlerFactory
    {
        TInterface CreateNew<TInterface>(SimpleInjectorInitializer.ImplementationType implementationType);

        void Register<TInterface, TImplementation>(SimpleInjectorInitializer.ImplementationType implementationType,
            Lifestyle lifestyle = null)
            where TImplementation : class, TInterface
            where TInterface : class;
    }
}