using System;
using Microsoft.Practices.Unity;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Unity
{
    public class UnityComponentContainer : IComponentRegistry, IComponentResolver
    {
        private readonly IUnityContainer _container;

        public UnityComponentContainer(IUnityContainer container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public object Resolve(Type serviceType)
        {
            Guard.AgainstNull(serviceType, "serviceType");

            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IComponentRegistry Register(Type serviceType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(implementationType, "implementationType");

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                    {
                        _container.RegisterType(serviceType, implementationType, new TransientLifetimeManager());

                        break;
                    }
                    default:
                    {
                        _container.RegisterType(serviceType, implementationType,
                            new ContainerControlledLifetimeManager());

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }
            return this;
        }

        public IComponentRegistry Register(Type serviceType, object instance)
        {
            Guard.AgainstNull(serviceType, "serviceType");
            Guard.AgainstNull(instance, "instance");

            try
            {
                _container.RegisterInstance(serviceType, instance);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }
    }
}