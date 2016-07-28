using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;
    using ActionFilters;
    using Common.Contracts.Enums;

    [AuthorizeActionFilter]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [AuthorizeActionFilter(UserType = UserType.Admin)]
        public async Task<ActionResult> Index()
        {
            List<Category> categoryList = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = JsonConvert.SerializeObject(categoryList);
            return View();
        }

        public async Task<JsonResult> GetAllProductsAsync()
        {
            var productList = await _productService.GetAllProductsAsync();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeActionFilter(UserType = UserType.Admin)]
        public async Task<JsonResult> AddProductAsync(Product product)
        {
            var productId = await _productService.AddProductAsync(product);
            return Json(productId);
        }

        [HttpPost]
        [AuthorizeActionFilter(UserType = UserType.Admin)]
        public async Task UpdateProductAsync(Product product)
        {
            await _productService.UpdateProductAsync(product);
        }

        [AuthorizeActionFilter(UserType = UserType.Admin)]
        public async Task DeleteProductAsync(Product product)
        {
            await _productService.DeleteProductAsync(product);
        }
    }
}