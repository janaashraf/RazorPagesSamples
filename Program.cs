using FluentMigrator.Runner;
using Samples.Migrations;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.Diagnostics;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddFluentMigratorCore()
  .ConfigureRunner(rb => rb
  .AddPostgres()
  .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
  .ScanIn(typeof(CreateInitialTables).Assembly).For.Migrations())
  .AddLogging(lb => lb.AddFluentMigratorConsole());
// Configure LLBLGen runtime
RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(
    c => c.SetTraceLevel(TraceLevel.Verbose)
          .AddDbProviderFactory(typeof(Npgsql.NpgsqlFactory))
);
RuntimeConfiguration.AddConnectionString("DefaultConnection", connectionString);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
