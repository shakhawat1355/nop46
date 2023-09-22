using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.UserInfo.Areas.Admin.Service;
using Nop.Plugin.Widgets.UserInfo.Factory;
using Nop.Plugin.Widgets.UserInfo.Model;
using Nop.Services.Security;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.UserInfo.Component
{
    public class UserViewComponent : NopViewComponent
    {

        private readonly IPermissionService _permissionService;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IUserService _userService;
        public UserViewComponent(IPermissionService permissionService,
            IUserModelFactory userModelFactory,
             IUserService userService
            )
        {
            _permissionService = permissionService;
            _userModelFactory = userModelFactory;
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var model = await _userService.GetAllUsersAsync(); 
            return View("~/Plugins/Widgets.UserInfo/Views/Public/PublicList.cshtml");
        }
    }
}
