using DocumentFormat.OpenXml.Spreadsheet;
using Nito.AsyncEx;
using Nop.Core.Domain.Customers;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.UserInfo.Domain;
using Nop.Services.Orders;
using Nop.Web.Areas.Admin.Validators.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Widgets.UserInfo.Areas.Admin.Service
{
    public class UserService : IUserService
    {

        protected readonly IRepository<User> _userRepository;




        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task InsertUserAsync(User user)
        {
            await _userRepository.InsertAsync(user);
        }



        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteAsync(user);
        }


        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }


        public async Task<IPagedList<User>> GetAllUsersAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            //var users = await _userRepository.GetAllPagedAsync(q =>
            //{
            //    return from u in q
            //           orderby u.Id
            //           where u.Id > 0
            //           select u;
            //} );

            var query = _userRepository.Table;
            query = query.Where(u => u.Id > 0);



            return await query.ToPagedListAsync(pageIndex, pageSize);
            //   return users;
        }

        public async Task<IPagedList<User>> SearchUserAsync(string userName="", string gender="", 
            int pageIndex = 0, int pageSize = int.MaxValue)
        {


            var query = _userRepository.Table;
            if(userName!= null)
            {
                query = query.Where(u => u.Name == userName);
            }
            if(gender!= null)
            {
                query = query.Where(u => u.Gender == gender);
            }

            return await query.ToPagedListAsync(pageIndex, pageSize);
       
        }

        public async Task<User> GetUserByIdAsync(int userID)
        {
            return await _userRepository.GetByIdAsync(userID);
        }

        public async Task<IList<User>> GetUsersByIdsAsync(int[] userIds)
        {
            return await _userRepository.GetByIdsAsync(userIds, includeDeleted: false);
        }

        public async Task DeleteUsersAsync(IList<User> users)
        {
            await _userRepository.DeleteAsync(users);
        }


    }
}
