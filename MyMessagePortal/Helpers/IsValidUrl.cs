using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MyMessagePortal.Helpers
{
    public class IsValidUrl : Controller
    {
        private readonly IHttpContextFactory _httpContextFactory;

        public IsValidUrl(IHttpContextFactory httpContextFactory)
        {
            _httpContextFactory = httpContextFactory;
        }

        public async Task<bool> Check(string path, string method)
        {
            IRouteCollection router = RouteData.Routers.OfType<IRouteCollection>().First();

            HttpContext context = _httpContextFactory.Create(HttpContext.Features);
            context.Request.Path = path;
            context.Request.Method = method;

            var routeContext = new RouteContext(context);
            await router.RouteAsync(routeContext);

            bool exists = routeContext.Handler != null;

            return exists;
        }
    }
}
