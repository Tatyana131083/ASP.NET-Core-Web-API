using FluentMigrator;

namespace MetricsAgent.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("cpu_metrics").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32().WithColumn("Time").AsInt64();
            Create.Table("dotnet_metrics").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32().WithColumn("Time").AsInt64();
            Create.Table("hdd_metrics").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32().WithColumn("Time").AsInt64();
            Create.Table("network_metrics").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32().WithColumn("Time").AsInt64();
            Create.Table("ram_metrics").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32().WithColumn("Time").AsInt64();
        }

        public override void Down()
        {
            Delete.Table("cpu_metrics");
            Delete.Table("dotnet_metrics");
            Delete.Table("hdd_metrics");
            Delete.Table("network_metrics");
            Delete.Table("ram_metrics");
        }
    }
}
