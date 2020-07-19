using System;
using System.Collections.Generic;
using Shuttle.Core.Container;
using Shuttle.Core.Contract;
using Unity;
using Unity.Lifetime;

namespace Shuttle.Core.Unity
{
    public class UnityComponentContainer : ComponentRegistry, IComponentResolver
    {
        private readonly IUnityContainer _container;

        public UnityComponentContainer(IUnityContainer container)
        {
            Guard.AgainstNull(container, "container");

            _container = container;
        }

        public object Resolve(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");

            try
            {
                return _container.Resolve(dependencyType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");

            try
            {
                return _container.ResolveAll(dependencyType);
            }
            catch (Exception ex)
            {
                throw new TypeResolutionException(ex.Message, ex);
            }
        }

        public override IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(implementationType, "implementationType");

            base.Register(dependencyType, implementationType, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            _container.RegisterType(dependencyType, implementationType, new TransientLifetimeManager());

                            break;
                        }
                    default:
                        {
                            _container.RegisterType(dependencyType, implementationType,
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

        public override IComponentRegistry RegisterCollection(Type dependencyType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(implementationTypes, "implementationTypes");

            base.RegisterCollection(dependencyType, implementationTypes, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            foreach (var implementationType in implementationTypes)
                            {
                                _container.RegisterType(dependencyType, implementationType, implementationType.FullName, new TransientLifetimeManager());
                            }

                            break;
                        }
                    default:
                        {
                            foreach (var implementationType in implementationTypes)
                            {
                                _container.RegisterType(dependencyType, implementationType, implementationType.FullName, new ContainerControlledLifetimeManager());
                            }

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

        public override IComponentRegistry RegisterInstance(Type dependencyType, object instance)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(instance, "instance");

            base.RegisterInstance(dependencyType, instance);

            try
            {
                _container.RegisterInstance(dependencyType, instance);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public override IComponentRegistry RegisterGeneric(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            return Register(dependencyType, implementationType, lifestyle);
        }
    }
}