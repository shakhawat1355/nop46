using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.UserInfo.Areas.Admin.Service;
using Nop.Plugin.Widgets.UserInfo.Domain;
using Nop.Plugin.Widgets.UserInfo.Factory;
using Nop.Plugin.Widgets.UserInfo.Model;
using Nop.Services.Media;
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
        #region fields
        private readonly IPermissionService _permissionService;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IUserService _userService;
        private readonly IPictureService _pictureService;
        #endregion


        #region ctor
        public UserController(IPermissionService permissionService,
            IUserModelFactory userModelFactory,
             IUserService userService,
             IPictureService pictureService
            )
        {
            _permissionService = permissionService;
            _userModelFactory = userModelFactory;
            _userService = userService;
            _pictureService = pictureService;
        }
        #endregion



        public async Task UpdatePictureNamesAsync(UserModel model)
        {
            var picture = await _pictureService.GetPictureByIdAsync(model.PictureId);
            if (picture != null)
                await _pictureService.SetSeoFilenameAsync(picture.Id, await _pictureService.GetPictureSeNameAsync(model.Name));
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
          //  await UpdatePictureNamesAsync(model);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await _userService.GetUserByIdAsync(id);
            await _userService.DeleteUserAsync(currentUser);
            return RedirectToAction("Index", "User");

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

            return Json(new { Result = true });
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


        public async Task<IActionResult> UserList()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();
             var model = await _userService.GetAllUsersAsync();

           
            return View("~/Plugins/Widgets.UserInfo/Views/Public/UserList.cshtml", model);
        }








    }
}


