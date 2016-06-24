using SimpleInjector;

namespace NEventStore.Common.Ioc.Interfaces
{
    public interface IRequestHandlerFactory
    {
        TInterface CreateNew<TInterface>(SimpleInjectorInitializerCommon.ImplementationType implementationType);

        void Register<TInterface, TImplementation>(SimpleInjectorInitializerCommon.ImplementationType implementationType,
            Lifestyle lifestyle = null)
            where TImplementation : class, TInterface
            where TInterface : class;
    }
}