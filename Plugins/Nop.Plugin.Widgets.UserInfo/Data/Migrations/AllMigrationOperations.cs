using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.UserInfo.Domain;
using Nop.Services.Localization;

namespace Nop.Plugin.Widgets.UserInfo.Data.Migrations
{
    [NopMigration("2023-05-25 08:40:55:1687599", "Other.userinfo base schema", MigrationProcessType.Installation)]

    public class AllMigrationOperations : Migration
    {
        private readonly ILocalizationService _localizationService;

        public AllMigrationOperations(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.TableFor<User>();
            if (Schema.Table("NS_WR_NopClient").Exists())
            {
                Create.Column(nameof(User.Name))
                .OnTable("NS_WR_NopClient")
                .AsString(255);
            }
            Rename.Table("ERProjectName").To("ERPProjectName");
            //_localizationService.AddOrUpdateLocaleResourceAsync("Plugin.Misc.WeeklyReport.StartDate", "From Date");
            //_localizationService.AddOrUpdateLocaleResourceAsync("Plugin.Misc.WeeklyReport.StartDate.Hint", "Pick a Date to start the month");
            //_localizationService.AddOrUpdateLocaleResourceAsync("Plugin.Misc.WeeklyReport.EndDate", "To Date");
            //_localizationService.AddOrUpdateLocaleResourceAsync("Plugin.Misc.WeeklyReport.EndDate.Hint", "Pick a date to end the month");

            Alter.Table("NS_WR_TeamMember").AddColumn("TrelloUserId").AsString().Nullable();
            Alter.Table("NS_WR_TeamMember").AddColumn(nameof(User.Name)).AsString().Nullable();
            Alter.Column(nameof(User.Name)).OnTable("NS_WR_TeamMember").AsInt32().Nullable();

        }
    }
}
