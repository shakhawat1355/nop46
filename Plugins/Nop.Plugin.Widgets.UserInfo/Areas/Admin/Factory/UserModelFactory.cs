using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Widgets.UserInfo.Domain;
using Nop.Plugin.Widgets.UserInfo.Model;
using Nop.Web.Areas.Admin.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using static ClosedXML.Excel.XLPredefinedFormat;
using Nop.Web.Framework.Models.Extensions;
using Nop.Plugin.Widgets.UserInfo.Areas.Admin.Service;
using DocumentFormat.OpenXml.Spreadsheet;
using Azure;

namespace Nop.Plugin.Widgets.UserInfo.Factory
{
    public class UserModelFactory : IUserModelFactory
    {

        public IUserService _userService { get; set; }


        public UserModelFactory(IUserService userService)
        {

            _userService = userService;
        }


        public async Task<UserItemListModel> PrepareUserListModelAsync(UserSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (searchModel.SearchName != null || searchModel.SearchGender != null)
            {
               var items = await _userService.SearchUserAsync(searchModel.SearchName, searchModel.SearchGender,
                pageSize: searchModel.PageSize,
                pageIndex: searchModel.Page - 1);


                var model = await new UserItemListModel().PrepareToGridAsync(searchModel, items, () =>
                {
                    return items.SelectAwait(async userItem =>
                    {
                        return await PrepareUserItemModelAsync(null, userItem, true);
                    });
                });


                return model;
            }
            else
            {
                var items = await _userService.GetAllUsersAsync(
                  pageSize: searchModel.PageSize,
                  pageIndex: searchModel.Page - 1);

                var model = await new UserItemListModel().PrepareToGridAsync(searchModel, items, () =>
                {
                    return items.SelectAwait(async userItem =>
                    {
                        return await PrepareUserItemModelAsync(null, userItem, true);
                    });
                });


                return model;
            }

            //var model = await new UserItemListModel().PrepareToGridAsync(searchModel, items, () =>
            //{
            //    return items.SelectAwait(async userItem =>
            //    {
            //        return await PrepareUserItemModelAsync(null, userItem, true);
            //    });
            //});


            //return model;


        }

        public async Task<UserModel> PrepareUserModelAsync(UserModel userModel)
        {
            if (userModel.Gender != null)
            {
                var newUser = new User();
                // newUser = userModel.ToEntity(newUser);
                Random rand = new Random();
                newUser.Id = rand.Next(100, 10000);
                newUser.Gender = userModel.Gender;
                newUser.Name = userModel.Name;
                newUser.DateOfBirth = userModel.DateOfBirth;
                newUser.Phone = userModel.Phone;
                newUser.CreationDate = System.DateTime.Now;
                newUser.PictureId = userModel.PictureId;
                await _userService.InsertUserAsync(newUser);
                return null;
            }
            userModel.GenderSelection = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select a gender", Value = "" },
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };

            return userModel;
        }

        public async Task<UserModel> PrepareUserModelEditAsync(User user)
        {
            var model = new UserModel();
            if (user.Gender != null)
            {
                model.Id = user.Id;
                model.Gender = user.Gender;
                model.Name = user.Name;
                model.DateOfBirth = user.DateOfBirth;
                model.Phone = user.Phone;
                model.GenderSelection = new List<SelectListItem>
                 {
                new SelectListItem { Text = "Select a gender", Value = "" },
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" }
                };
                model.PictureId = user.PictureId;
            }

            return model;

        }

        public UserSearchModel PrepareUserSearchModelAsync(UserSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();

            return searchModel;
        }


        public virtual async Task<UserModel> PrepareUserItemModelAsync(UserModel model, User user, bool excludeProperties = false)
        {

            if (user != null)
            {
                if (model == null)
                {
                    model = new UserModel()
                    {
                        Name = user.Name,
                        DateOfBirth = user.DateOfBirth,
                        Phone = user.Phone,
                        Id = user.Id,
                        Gender = user.Gender
                    };
                }
            }
            return model;
        }

        public UserModel PrepareUserItemSearchModel(UserModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();

            return searchModel;
        }

        public async Task<IList<UserModel>> PreparePublicUserListModel(IList<User> Users)
        {
            var models = new List<UserModel>();

            foreach(var user in Users)
            {
                var item = await PrepareUserModelEditAsync(user);
                item.Url = ImgUrlBuilder(item.PictureId);
                models.Add(item);
            }

            return models;
        }

        public string ImgUrlBuilder(int imgNo)
        {
            if(imgNo != 0)
            {
                return "https://localhost:44369/images/thumbs/0000" + imgNo.ToString() + "_Avatar_100.jpeg";

            }
            else
            {
                return "https://localhost:44369/images/thumbs/default-image_80.png";

            }
        }




    }
}
