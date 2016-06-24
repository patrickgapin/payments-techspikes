using System;
using System.Collections.Generic;
using NEventStore.Common.Ioc.Interfaces;
using SimpleInjector;

namespace NEventStore.Common.Ioc
{
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly Dictionary<SimpleInjectorInitializer.ImplementationType, InstanceProducer> _producers =
           new Dictionary<SimpleInjectorInitializer.ImplementationType, InstanceProducer>();

        private readonly Container _container;

        public RequestHandlerFactory(Container container)
        {
            _container = container;
        }

        public TInterface CreateNew<TInterface>(SimpleInjectorInitializer.ImplementationType implementationType)
        {
            if (!_producers.ContainsKey(implementationType))
                throw new ArgumentException("Instance Name does not exist in dictionary.");

            return (TInterface)_producers[implementationType].GetInstance();
        }

        public void Register<TInterface, TImplementation>(SimpleInjectorInitializer.ImplementationType implementationType,
                Lifestyle lifestyle = null)
            where TImplementation : class, TInterface
            where TInterface : class
        {
            lifestyle = lifestyle ?? Lifestyle.Transient;

            var producer = lifestyle
                .CreateProducer<TInterface, TImplementation>(_container);

            _producers.Add(implementationType, producer);
        }
    }
}