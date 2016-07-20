using System;
using System.Collections.Generic;
using PinnacleSports.Shared.RuleEngineFactory.Interfaces;
using SimpleInjector;

namespace PinnacleSports.Shared.RuleEngineFactory
{
    public class RuleEngineFactory : IRuleEngineFactory
    {
        private readonly Dictionary<RuleEngineTypes.ImplementationType, InstanceProducer> _producers =
           new Dictionary<RuleEngineTypes.ImplementationType, InstanceProducer>();

        private readonly Container _container;

        public RuleEngineFactory(Container container)
        {
            _container = container;
        }

        public TInterface CreateNew<TInterface>(RuleEngineTypes.ImplementationType implementationType)
        {
            if (!_producers.ContainsKey(implementationType))
                throw new ArgumentException("Instance Name does not exist in dictionary.");

            return (TInterface)_producers[implementationType].GetInstance();
        }

        public void Register<TInterface, TImplementation>(RuleEngineTypes.ImplementationType implementationType,
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