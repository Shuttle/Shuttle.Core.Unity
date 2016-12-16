using Microsoft.Practices.Unity;
using NUnit.Framework;
using Shuttle.Core.ComponentContainer.Tests;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Unity.Tests
{
    [TestFixture]
    public class UnityComponentContainerFixture : ComponentContainerFixture
    {
        [Test]
        public void Should_be_able_resolve_all_instances()
        {
            var container = new UnityComponentContainer(new UnityContainer());

            RegisterCollection(container);
            ResolveCollection(container);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_singleton()
        {
            var container = new UnityComponentContainer(new UnityContainer());

            RegisterSingleton(container);
            ResolveSingleton(container);
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