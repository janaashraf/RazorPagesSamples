// Migrations/CreateInitialTables.cs
using FluentMigrator;

namespace RazorFluentMigratorSample.Migrations
{
    [Migration(1)]
    public class CreateUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
