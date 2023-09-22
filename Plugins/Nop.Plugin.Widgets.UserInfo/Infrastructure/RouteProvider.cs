using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace Nop.Plugin.Widgets.UserInfo.Infrastructure
{
    public class RouteProvider : BaseRouteProvider, IRouteProvider
    {
        public int Priority => 1;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var lang = GetLanguageRoutePattern();

            endpointRouteBuilder.MapControllerRoute(name: "UserDetailsViewer",
            pattern: $"User/Details",
           defaults: new { controller = "Widget", action = "UserList" });

            endpointRouteBuilder.MapControllerRoute(name: "UserDetailViewerPaged",
                pattern: $"User/Details/page/{{pageNumber:min(0)}}",
                defaults: new { controller = "Widget", action = "UserList" });
        }
    }
}
