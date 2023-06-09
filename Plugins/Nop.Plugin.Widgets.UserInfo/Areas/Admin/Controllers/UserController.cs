using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.UserInfo.Areas.Admin.Service;
using Nop.Plugin.Widgets.UserInfo.Domain;
using Nop.Plugin.Widgets.UserInfo.Factory;
using Nop.Plugin.Widgets.UserInfo.Model;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.UserInfo.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]

    public class UserController : BasePluginController
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IUserService _userService;
        public UserController(IPermissionService permissionService,
            IUserModelFactory userModelFactory,
             IUserService userService
            )
        {
            _permissionService = permissionService;
            _userModelFactory = userModelFactory;
            _userService = userService;
        }
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();


            return View("~/Plugins/Widgets.UserInfo/Views/Configure.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();

            var model = await _userModelFactory.PrepareUserModelAsync(new UserModel());

           return View("~/Plugins/Widgets.UserInfo/Areas/Admin/Views/User/create.cshtml", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel model)
        {

            var obj = await _userModelFactory.PrepareUserModelAsync(model);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await _userService.GetUserByIdAsync(id);
            await _userService.DeleteUserAsync(currentUser);
            return RedirectToAction("Index", "User");

        }

        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = await _userService.GetUserByIdAsync(id);
            var model = await _userModelFactory.PrepareUserModelEditAsync(currentUser);
            return View("~/Plugins/Widgets.UserInfo/Areas/Admin/Views/User/Edit.cshtml", model);
         
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Edit(User model, bool continueEditing)
        {
            
            if(continueEditing)
            {
                await _userService.UpdateUserAsync(model);
                return RedirectToAction("Edit", "User");
            }
           await _userService.UpdateUserAsync(model);
           return RedirectToAction("Index", "User");

        }

        public virtual async Task<IActionResult> Index()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();

            return RedirectToAction("List");
        }

        public async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();


            var model = _userModelFactory.PrepareUserSearchModelAsync(new UserSearchModel());
            return View("~/Plugins/Widgets.UserInfo/Areas/Admin/Views/User/List.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(UserSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return await AccessDeniedDataTablesJson();

            var model = await _userModelFactory.PrepareUserListModelAsync(searchModel);
            return Json(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageManufacturers))
                return AccessDeniedView();

            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            var users = await _userService.GetUsersByIdsAsync(selectedIds.ToArray());
            await _userService.DeleteUsersAsync(users);

            //var locale = await _localizationService.GetResourceAsync("ActivityLog.DeleteManufacturer");
            //foreach (var manufacturer in manufacturers)
            //{
            //    //activity log
            //    await _customerActivityService.InsertActivityAsync("DeleteManufacturer", string.Format(locale, manufacturer.Name), manufacturer);
            //}

            return Json(new { Result = true });
        }










    }
}


