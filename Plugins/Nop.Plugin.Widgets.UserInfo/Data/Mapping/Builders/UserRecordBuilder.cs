using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.UserInfo.Domain;

namespace Nop.Plugin.Widgets.UserInfo.Data.Mapping.Builders
{
    public class UserRecordBuilder : NopEntityBuilder<User>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(User.Id)).AsInt32().PrimaryKey().
             WithColumn(nameof(User.Name)).AsString(100)

            .WithColumn(nameof(User.DateOfBirth)).AsDate()
            .WithColumn(nameof(User.Gender)).AsString(10)
            .WithColumn(nameof(User.Phone)).AsString(10)
            .WithColumn(nameof(User.CreationDate)).AsDate();
        }
    }

}
