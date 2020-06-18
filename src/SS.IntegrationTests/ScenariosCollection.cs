using Xunit;

namespace SS.IntegrationTests
{
    [CollectionDefinition("Scenarios")]
    public class ScenariosCollection : ICollectionFixture<SSFixture>
    {
    }
}
