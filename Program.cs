using FluentMigrator.Runner;
using RazorFluentMigratorSample.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddFluentMigratorCore()
  .ConfigureRunner(rb => rb
  .AddPostgres()
  .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
  .ScanIn(typeof(CreateUserTable).Assembly).For.Migrations())
  .AddLogging(lb => lb.AddFluentMigratorConsole());

var app = builder.Build();
// Run the migrations
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
