using System;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System.Threading.Tasks;
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;

    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Categories
        public async Task<JsonResult> GetCategories()
        {
            var categories = await this._categoryService.GetAllCategoriesAsync();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddCategory(Category category)
        {
            var categoryId = await this._categoryService.AddCategoryAsync(category);
            return Json(categoryId);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            Category category = await this._categoryService.GetCategoryAsync(categoryId);
            await this._categoryService.DeleteCategoryAsync(category);
            return Json("OK");
        }
    }
}