using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Common;

namespace Nop.Plugin.Widgets.UserInfo.Model
{
    public record UserListModel : BaseNopModel
    {
        public UserListModel()
        {
            Users = new List<UserModel>();
        }
        public IList<UserModel> Users { get; set; }
        public PagerModel PagerModel { get; set; }

        public partial record UserListRouteValues : IRouteValues
        {
            public int PageNumber { get; set; }
        }

    }


}
