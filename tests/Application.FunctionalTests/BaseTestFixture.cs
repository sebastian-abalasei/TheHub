namespace TheHub.Application.FunctionalTests;

#region

using static Testing;

#endregion

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
