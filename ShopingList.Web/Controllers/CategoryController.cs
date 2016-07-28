using System;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System.Threading.Tasks;
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;
    using ActionFilters;
    using Common.Contracts.Enums;

    [AuthorizeActionFilter(UserType = UserType.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Categories
        public async Task<JsonResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddCategoryAsync(Category category)
        {
            var categoryId = await _categoryService.AddCategoryAsync(category);
            return Json(categoryId);
        }

        [HttpPost]
        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryService.UpdateCategoryAsync(category);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCategoryAsync(Guid categoryId)
        {
            Category category = await _categoryService.GetCategoryAsync(categoryId);
            await _categoryService.DeleteCategoryAsync(category);
            return Json("OK");
        }
    }
}