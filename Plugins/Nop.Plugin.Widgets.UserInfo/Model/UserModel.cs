using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.UserInfo.Model
{
    public record UserModel : BaseSearchModel
    {
        public int Id { get; set; }


        [NopResourceDisplayName("Plugins.Widgets.Crud.Name")]
        public string Name { get; set; }

        [UIHint("DateNullable")]
        [NopResourceDisplayName("Plugins.Widgets.Crud.DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Crud.Gender")]
        public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [NopResourceDisplayName("Plugins.Widgets.Crud.Phone")]
        public string Phone { get; set; }

        [UIHint("DateOnly")]
        [NopResourceDisplayName("User.CreationDate")]
        public DateTime CreationDate { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName("Admin.Catalog.Categories.Fields.Picture")]
        public int PictureId { get; set; }

        public string Url { get; set; }

        public IList<SelectListItem> GenderSelection { get; set; }





    }
}
