using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EternalStore.Api.Contracts.Goods.Requests;

namespace EternalStore.Api.Controllers
{
    [Route("api/store/")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsManager goodsManager;

        public GoodsController(IGoodsManager goodsManager) => this.goodsManager = goodsManager;

        [HttpGet("categories", Name = "GetCategories")]
        public async Task<IActionResult> Get()
        {
            var categories = await goodsManager.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{idCategory}", Name = "GetCategory")]
        public async Task<IActionResult> Get(int idCategory)
        {
            var category = await goodsManager.GetCategoryAsync(idCategory);
            return Ok(category);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPost("categories", Name = "AddCategory")]
        public async Task<IActionResult> Post([FromBody] CategoryCreationRequest request)
        {
            await goodsManager.CreateCategoryAsync(request.Name);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPut("categories/{idCategory}", Name = "EditCategory")]
        public async Task<IActionResult> Put([FromBody] CategoryModificationRequest request)
        {
            await goodsManager.UpdateCategoryAsync(request.IdCategory, request.Name);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpDelete("categories/{idCategory}", Name = "EnableDisableCategory")]
        public async Task<IActionResult> Delete(int idCategory)
        {
            var category = await goodsManager.GetCategoryAsync(idCategory);

            if (category.IsEnabled)
                await goodsManager.DisableCategoryAsync(idCategory);

            if (!category.IsEnabled)
                await goodsManager.EnableCategoryAsync(idCategory);

            return Ok();
        }

        [HttpGet("categories/{idCategory}/products", Name = "GetProductsForCategory")]
        public async Task<IActionResult> GetProductsForCategory(int idCategory)
        {
            var productsForCategory = await goodsManager.GetProductsForCategoryAsync(idCategory);
            return Ok(productsForCategory);
        }

        [HttpGet("categories/{idCategory}/products/{idProduct}", Name = "GetProduct")]
        public async Task<IActionResult> Get(int idCategory, int idProduct)
        {
            var product = await goodsManager.GetProductAsync(idCategory, idProduct);
            return Ok(product);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPost("categories/{idCategory}/products", Name = "AddProduct")]
        public async Task<IActionResult> Post([FromBody] ProductCreationRequest request)
        {
            await goodsManager.AddProductAsync(request.IdCategory, request.Name, request.Description, request.Price);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPut("categories/{idCategory}/products/{idProduct}", Name = "EditProduct")]
        public async Task<IActionResult> Put([FromBody] ProductModificationRequest request)
        {
            await goodsManager.EditProductAsync(request.IdCategory, request.IdProduct, request.Name, request.Description, request.Price);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpDelete("categories/{idCategory}/products/{idProduct}", Name = "RemoveProduct")]
        public async Task<IActionResult> Delete(int idCategory, int idProduct)
        {
            await goodsManager.RemoveProductAsync(idCategory, idProduct);
            return Ok();
        }
    }
}
