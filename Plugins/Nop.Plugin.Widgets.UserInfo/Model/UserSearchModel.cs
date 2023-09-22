using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.UserInfo.Model
{
    public record UserSearchModel : BaseSearchModel
    {
        public UserSearchModel()
        {

            SearchGenderSelection = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select a gender", Value = "" },
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };

        }

        [NopResourceDisplayName("Plugins.Widgets.Crud.SearchName")]
        public string SearchName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Crud.SearchGender")]
        public string SearchGender { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Crud.SearchGenderSelection")]
        public IList<SelectListItem> SearchGenderSelection { get; set; }



    }
}
