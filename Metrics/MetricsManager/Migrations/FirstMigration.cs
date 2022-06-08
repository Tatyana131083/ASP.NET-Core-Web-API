using FluentMigrator;

namespace MetricsManager.Migrations
{
    [Migration(0)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("agents").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Url").AsString();
        }

        public override void Down()
        {
            Delete.Table("agents");
        }
    }
}
