using System;
using System.Web;

namespace ShopingList.Web.ActionFilters
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Common.Contracts.DataContracts;
    using Common.Contracts.Enums;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeActionFilterAttribute : ActionFilterAttribute
    {
        public UserType UserType = UserType.Both;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                var user = session?["user"] as User;
                if (user != null && (UserType == UserType.Both || user.Type == UserType))
                    base.OnActionExecuting(filterContext);
                else
                {
                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                }
            }
            else
            {

                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }
        }
    }
}