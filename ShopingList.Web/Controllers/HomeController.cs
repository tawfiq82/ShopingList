using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System.Threading.Tasks;
    using Common.Contracts.DataContracts;
    using Common.Contracts.Enums;
    using Common.Contracts.ServiceContracts;

    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Home
        public ActionResult Index()
        {
            User user = Session["user"] as User;
            if (user == null)
            {
                User model = new User();
                return View(model);
            }
            if (user.Type == UserType.Normal)
                return RedirectToAction("Index", "ShoppingItems");
            return RedirectToAction("Index", "Product");
        }

        public async Task<ActionResult> Login(User model)
        {
            var user = await _userService.GetUserAsync(model.Name);
            if (user != null)
            {
                Session["user"] = user;
                if (user.Type == UserType.Normal)
                    return RedirectToAction("Index", "ShoppingItems");
                return RedirectToAction("Index", "Product");
            }
            return Json("Please enter a valid user name");
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}