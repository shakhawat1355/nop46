using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.UserInfo.Areas.Admin.Service;
using Nop.Plugin.Widgets.UserInfo.Factory;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.UserInfo.Controllers
{
    public class WidgetController : BasePluginController
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        private readonly IUserModelFactory _userModelFactory;

        public WidgetController(IPermissionService permissionService, IUserService userService, IUserModelFactory userModelFactory)
        {
            _permissionService = permissionService;
            _userService = userService;
            _userModelFactory = userModelFactory;

        }



        public async Task<IActionResult> UserList(int? pageNumber)
        {

            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();
      
            var model= await _userModelFactory.PreparePublicUserListModel(pageNumber);


            return View("~/Plugins/Widgets.UserInfo/Views/Public/UserList.cshtml", model);
        }




    }
}
