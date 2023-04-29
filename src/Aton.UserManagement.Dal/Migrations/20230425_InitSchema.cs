using FluentMigrator;

namespace Aton.UserManagement.Dal.Migrations;

[Migration(20230425, TransactionBehavior.None)]
public class InitSchema : Migration
{
    public override void Up()
    {
        Create.Table("aton_user")
            .WithColumn("guid").AsGuid().PrimaryKey("user_pk").Identity()
            .WithColumn("login").AsString().NotNullable().Unique()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("gender").AsInt32().NotNullable()
            .WithColumn("birthday").AsDateTime().Nullable()
            .WithColumn("admin").AsBoolean().NotNullable()
            .WithColumn("created_on").AsDateTime().NotNullable()
            .WithColumn("created_by").AsString().NotNullable()
            .WithColumn("modified_on").AsDateTime().Nullable()
            .WithColumn("modified_by").AsString().Nullable()
            .WithColumn("revoked_on").AsDateTime().Nullable()
            .WithColumn("revoked_by").AsString().Nullable()
            ;

        Create.Index("login_index")
            .OnTable("aton_user")
            .OnColumn("login");
    }

    public override void Down()
    {
        Delete.Table("aton_user");
    }
}