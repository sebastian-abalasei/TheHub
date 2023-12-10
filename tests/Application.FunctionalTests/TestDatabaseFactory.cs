namespace TheHub.Application.FunctionalTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        SqliteTestDatabase database = new SqliteTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}
