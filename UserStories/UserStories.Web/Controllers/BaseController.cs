using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UserStories.Web.Utilities;

namespace UserStories.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public long UserId { get { return SessionManager.UserInfo.Id; } }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (SessionManager.UserInfo == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                           new RouteValueDictionary(
                               new
                               {
                                   controller = "Account",
                                   action = "Login"
                               })
                           );
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
