using NUnit.Framework;
using Shuttle.Core.Container.Tests;
using Unity;

namespace Shuttle.Core.Unity.Tests
{
    [TestFixture]
    public class UnityComponentContainerFixture : ContainerFixture
    {
        [Test]
        public void Should_be_able_resolve_all_instances()
        {
            var container = new UnityComponentContainer(new UnityContainer());

            RegisterCollection(container);
            ResolveCollection(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_multiple_singleton()
        {
            var container = new UnityComponentContainer(new UnityContainer());

            RegisterMultipleSingleton(container);
            ResolveMultipleSingleton(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_singleton()
        {
            var container = new UnityComponentContainer(new UnityContainer());

            RegisterSingleton(container);
            ResolveSingleton(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_multiple_transient_components()
        {
            var container = new UnityComponentContainer(new UnityContainer());

            RegisterMultipleTransient(container);
            ResolveMultipleTransient(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_transient_components()
        {
            var container = new UnityComponentContainer(new UnityContainer());

            RegisterTransient(container);
            ResolveTransient(container);
        }
    }
}