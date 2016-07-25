using System;
using System.Net;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System.Threading.Tasks;
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;

    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Users
        public async Task<JsonResult> GetUsers()
        {
            var users = await this._userService.GetAllUsersAsync();
            return Json(users, JsonRequestBehavior.AllowGet);
        }



        // GET: Users/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = await this._userService.GetUserAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddUser(User user)
        {
            var userId = await this._userService.AddUserAsync(user);
            return Json(userId);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await this._userService.GetUserAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Name,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                await this._userService.UpdateUserAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            User user = await this._userService.GetUserAsync(userId);
            await _userService.DeleteUserAsync(user);
            return Json("OK");
        }
    }
}
