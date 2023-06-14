using Nop.Services.Cms;
using Nop.Services.Plugins;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Nop.Web.Framework.Menu;
using Microsoft.AspNetCore.Routing;
using Nop.Services.Security;
using Nop.Web.Framework;
using System.Linq;
using Nop.Core;
using Microsoft.Extensions.FileProviders;
using Nop.Services.Localization;
using Nop.Web.Framework.Infrastructure;
using Nop.Plugin.Widgets.UserInfo.Component;

namespace Nop.Plugin.Widgets.UserInfo
{
    public class UserInfoPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        public bool HideInWidgetList => false;
        private readonly IWebHelper _webHelper;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        public UserInfoPlugin(IWebHelper webHelper,
            IPermissionService permissionService, 
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _permissionService = permissionService;
            _localizationService = localizationService;
        }

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(UserViewComponent);
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
           return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.AccountNavigationAfter });
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return;

            var menuItem = new SiteMapNode()
            {
                Title = "Users",
                Visible = true,
                IconClass = "far fa-circle",
                SystemName = "UserInfo"
            };
            rootNode.ChildNodes.Add(menuItem);

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "UserInfo");
           pluginNode.ChildNodes.Add(new SiteMapNode()
           {
               Title = "List",
                Url = $"{_webHelper.GetStoreLocation()}Admin/User/List",
               Visible = true,
               IconClass = "far fa-dot-circle",
               SystemName = "UserList"
           });

        }
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.Crud.EditButton"] = "Edit",
                ["Plugins.Widgets.Crud.IndexPageTitle"] = "Users list",
                ["Plugins.Widgets.Crud.EditPageTitle"] = "Edit user info",
                ["Plugins.Widgets.Crud.CreatePageTitle"] = "Add new user",
                ["Plugins.Widgets.Crud.BackToList"] = "back to user list",
                ["Plugins.Widgets.Crud.Name"] = "Name",
                ["Plugins.Widgets.Crud.Name.Hint"] = "Enter user's name",
                ["Plugins.Widgets.Crud.DateOfBirth"] = "Date of birth",
                ["Plugins.Widgets.Crud.DateOfBirth.Hint"] = "Enter user's date Of birth",
                ["Plugins.Widgets.Crud.Gender"] = "Gender",
                ["Plugins.Widgets.Crud.Gender.Hint"] = "Enter user's gender",
                ["Plugins.Widgets.Crud.Phone"] = "Phone",
                ["Plugins.Widgets.Crud.Phone.Hint"] = "Entuser's phone",
                ["Plugins.Widgets.Crud.SearchName"] = "Search name",
                ["Plugins.Widgets.Crud.SearchGender"]= "Search gender",
                ["Plugins.Widgets.Crud.SearchName.Hint"] = "Provide user name",
                ["Plugins.Widgets.Crud.SearchGender.Hint"] = "Provide user gender",
                ["Plugins.Widgets.Crud.SearchGenderSelection"] = "Search gender selection",
                ["Plugins.Widgets.Crud.SearchGenderSelection.Hint"] = "Provide gender",
                ["Plugins.Widgets.Crud.Fields.Picture"] = "Upload User's Picture"


            });
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.Crud");
            await base.UninstallAsync();
        }




    }

}