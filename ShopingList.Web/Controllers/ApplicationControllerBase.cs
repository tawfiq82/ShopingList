using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using Common.Contracts.DataContracts;

    public class ApplicationControllerBase : Controller
    {
        private User _sessionUser;

        protected User SessionUser
        {
            get
            {
                if (_sessionUser == null)
                {
                    var user = Session["user"] as User;
                    if (user != null)
                    {
                        _sessionUser = user;
                    }
                    else
                    {
                        RedirectToAction("Index", "Home");
                    }
                }
                return _sessionUser;
            }
        }
    }
}