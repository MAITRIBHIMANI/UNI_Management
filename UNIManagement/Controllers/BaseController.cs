using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace UNIManagement.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Page not fould - error page
        /// </summary>
        /// <returns>Display Page not found page</returns>
        public ActionResult PageNotFoundOnSamePage()
        {
            string html = "404";
            this.Response.ContentType = "text/html";
            this.Response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
            return Content(html);
        }

        /// <summary>
        /// Get Current Route Name
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCurrentRouteName()
        {
            return ControllerContext.ActionDescriptor.AttributeRouteInfo.Name;
        }

        /// <summary>
        /// Get Current Route Name
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCurrentPath()
        {
            return ControllerContext.HttpContext.Request.Path;
        }
    }
}
