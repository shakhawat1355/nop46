using AutoMapper;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.UserInfo.Domain;
using Nop.Plugin.Widgets.UserInfo.Model;
using Nop.Web.Areas.Admin.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.UserInfo.Data.Mapping
{
    public class UserAutoMapperConfig : Profile, IOrderedMapperProfile
    {
        public int Order => 10;

        public UserAutoMapperConfig() {
            UserCustomMapper();
        }

        public void UserCustomMapper()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
