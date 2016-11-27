namespace Shuttle.Core.Unity.Tests
{
    public class DoSomething : IDoSomething
    {
        public ISomeDependency SomeDependency {
            get { return null; }
        }
    }
}