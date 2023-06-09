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

namespace Nop.Plugin.Widgets.UserInfo.Factory
{
    public interface IUserModelFactory
    {
        public UserSearchModel PrepareUserSearchModelAsync(UserSearchModel searchModel);

        Task<UserModel> PrepareUserModelAsync(UserModel userModel);

        Task<UserModel> PrepareUserModelEditAsync(User user);

        Task<UserModel> PrepareUserItemModelAsync(UserModel model, User user, bool excludeProperties = false);

        UserModel PrepareUserItemSearchModel(UserModel searchModel); 

        Task<UserItemListModel> PrepareUserListModelAsync(UserSearchModel searchModel);


    }
}
