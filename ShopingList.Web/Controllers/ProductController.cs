using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopingList.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Common.Contracts.DataContracts;
    using Common.Contracts.ServiceContracts;

    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<ActionResult> Index()
        {
            List<Category> categoryList = await this._categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = JsonConvert.SerializeObject(categoryList);
            return View();
        }

        public async Task<JsonResult> GetAllProductsAsync()
        {
            var productList = await this._productService.GetAllProductsAsync();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddProductAsync(Product product)
        {
            var productId = await this._productService.AddProductAsync(product);
            return Json(productId);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await _productService.DeleteProductAsync(product);
        }
    }
}