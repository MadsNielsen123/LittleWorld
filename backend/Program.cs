using Dapper;
using DbUp;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
var baseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
var databasePassword = builder.Configuration["DatabasePassword"]
    ?? throw new InvalidOperationException("Configuration value 'DatabasePassword' is missing.");
var connectionStringBuilder = new MySqlConnectionStringBuilder(baseConnectionString)
{
    Password = databasePassword
};
var connectionString = connectionStringBuilder.ConnectionString;

var app = builder.Build();

RunMigrations(connectionString, app.Environment.ContentRootPath);

app.MapGet("/api/message", async () =>
{
    await using var connection = new MySqlConnection(connectionString);
    const string sql = "SELECT Value FROM AppMessages ORDER BY Id LIMIT 1;";
    var message = await connection.QueryFirstOrDefaultAsync<string>(sql);

    return Results.Ok(new { message = message ?? "No message found in database." });
});

app.Run();

static void RunMigrations(string connectionString, string contentRootPath)
{
    var scriptsPath = Path.Combine(contentRootPath, "Scripts");
    var upgrader = DeployChanges.To
        .MySqlDatabase(connectionString)
        .WithScriptsFromFileSystem(scriptsPath)
        .Build();

    var result = upgrader.PerformUpgrade();
    if (!result.Successful)
    {
        throw result.Error;
    }
}
