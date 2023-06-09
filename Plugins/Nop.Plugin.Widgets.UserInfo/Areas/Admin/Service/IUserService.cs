using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.UserInfo.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.UserInfo.Areas.Admin.Service
{
    public interface IUserService
    {
        Task InsertUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<IPagedList<User>> GetAllUsersAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

        Task<IPagedList<User>> SearchUserAsync(string userName, string Gender, int pageIndex = 0, int pageSize = int.MaxValue);

        Task<IList<User>> GetUsersByIdsAsync(int[] userIds);
        Task DeleteUsersAsync(IList<User> users);

    }
}
