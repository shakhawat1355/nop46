using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.UserInfo.Domain;

namespace Nop.Plugin.Widgets.UserInfo.Data.Migrations
{

    [NopMigration("2023-05-25 08:40:55:1687599", "Other.userinfo base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<User>();
        }
    }
}
