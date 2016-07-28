using System;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;
    using Newtonsoft.Json;
    using ActionFilters;
    using Common.Contracts.Enums;

    [AuthorizeActionFilter(UserType = UserType.Normal)]
    public class ShoppingItemsController : ApplicationControllerBase
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
            List<Category> categoryList = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = JsonConvert.SerializeObject(categoryList);
            return View();
        }

        public async Task<ActionResult> GetShoppingItemsAsync()
        {
            var shoppingItems = await _shoppingItemService.GetShoppingItemsAsync(SessionUser);
            return Json(shoppingItems, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddShoppingItemAsync(Product product)
        {
            var shoppingItemId = await _shoppingItemService.AddShoppingItemAsync(SessionUser, product);
            return Json(shoppingItemId);
        }

        public async Task RemoveShoppingItemAsync(Guid shoppingItemId)
        {
            await _shoppingItemService.RemoveShoppingItemAsync(shoppingItemId);
        }

        public async Task ClearAllShoppingItemsAsync()
        {
            await _shoppingItemService.ClearAllShoppingItemsAsync(SessionUser);
        }

        public async Task CheckOutShoppingItemsAsync(IList<ShopingItem> shoppingItems)
        {
            await _shoppingItemService.CheckOutShoppingItemsAsync(shoppingItems);
        }
    }
}