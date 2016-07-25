using System;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;
    using Newtonsoft.Json;

    public class ShoppingItemsController : Controller
    {
        private readonly IShoppingItemService _shoppingItemService;
        private readonly ICategoryService _categoryService;

        public ShoppingItemsController(IShoppingItemService shoppingItemService, ICategoryService categoryService)
        {
            _shoppingItemService = shoppingItemService;
            _categoryService = categoryService;
        }

        public async Task<ActionResult> Index()
        {
            List<Category> categoryList = await this._categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = JsonConvert.SerializeObject(categoryList);
            return View();
        }

        //public async Task<ActionResult> ShowShoppingItems()
        //{
        //    List<Category> categoryList = await this._categoryService.GetAllCategoriesAsync();
        //    ViewBag.CategoryList = JsonConvert.SerializeObject(categoryList);
        //    return View();
        //}

        public async Task<ActionResult> GetShoppingItemsAsync()
        {
            User user = Session["user"] as User;

            var shoppingItems = await this._shoppingItemService.GetShoppingItemsAsync(user);
            return Json(shoppingItems, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddShoppingItemAsync(Product product)
        {
            User user = Session["user"] as User;
            var shoppingItemId = await this._shoppingItemService.AddShoppingItemAsync(user, product);
            return Json(shoppingItemId);
        }

        public async Task RemoveShoppingItemAsync(Guid shoppingItemId)
        {
            await this._shoppingItemService.RemoveShoppingItemAsync(shoppingItemId);
        }

        public async Task ClearAllShoppingItemsAsync()
        {
            User user = Session["user"] as User;
            await this._shoppingItemService.ClearAllShoppingItemsAsync(user);
        }

        public async Task CheckOutShoppingItemsAsync(IList<ShopingItem> shoppingItems)
        {
            await this._shoppingItemService.CheckOutShoppingItemsAsync(shoppingItems);
        }
    }
}