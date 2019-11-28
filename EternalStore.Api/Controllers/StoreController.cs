using EternalStore.Api.Contracts.Store.Requests;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EternalStore.Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreManager storeManager;

        public StoreController(IStoreManager storeManager) => this.storeManager = storeManager;

        [HttpGet("categories", Name = "GetCategories")]
        public async Task<IActionResult> Get()
        {
            var categories = await storeManager.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{idCategory}", Name = "GetCategory")]
        public async Task<IActionResult> Get(int idCategory)
        {
            var category = await storeManager.GetCategoryAsync(idCategory);
            return Ok(category);
        }

        [HttpPost("categories", Name = "AddCategory")]
        public async Task<IActionResult> Post([FromBody] CategoryCreationRequest request)
        {
            await storeManager.CreateCategoryAsync(request.Name);
            return Ok();
        }

        [HttpPut("categories/{idCategory}", Name = "EditCategory")]
        public async Task<IActionResult> Put([FromBody] CategoryModificationRequest request)
        {
            await storeManager.UpdateCategoryAsync(request.IdCategory, request.Name);
            return Ok();
        }

        [HttpDelete("categories/{idCategory}", Name = "EnableDisableCategory")]
        public async Task<IActionResult> Delete(int idCategory)
        {
            var category = await storeManager.GetCategoryAsync(idCategory);

            if (category.IsEnabled)
                await storeManager.DisableCategoryAsync(idCategory);

            if (!category.IsEnabled)
                await storeManager.EnableCategoryAsync(idCategory);

            return Ok();
        }

        [HttpGet("categories/{idCategory}/products", Name = "GetProductsForCategory")]
        public async Task<IActionResult> GetProductsForCategory(int idCategory)
        {
            var productsForCategory = await storeManager.GetProductsForCategoryAsync(idCategory);
            return Ok(productsForCategory);
        }

        [HttpGet("categories/{idCategory}/products/{idProduct}", Name = "GetProduct")]
        public async Task<IActionResult> Get(int idCategory, int idProduct)
        {
            var product = await storeManager.GetProductAsync(idCategory, idProduct);
            return Ok(product);
        }

        [HttpPost("categories/{idCategory}/products", Name = "AddProduct")]
        public async Task<IActionResult> Post([FromBody] ProductCreationRequest request)
        {
            await storeManager.AddProductAsync(request.IdCategory, request.Name, request.Description, request.Price);
            return Ok();
        }

        [HttpPut("categories/{idCategory}/products/{idProduct}", Name = "EditProduct")]
        public async Task<IActionResult> Put([FromBody] ProductModificationRequest request)
        {
            await storeManager.EditProductAsync(request.IdCategory, request.IdProduct, request.Name, request.Description, request.Price);
            return Ok();
        }

        [HttpDelete("categories/{idCategory}/products/{idProduct}", Name = "RemoveProduct")]
        public async Task<IActionResult> Delete(int idCategory, int idProduct)
        {
            await storeManager.RemoveProductAsync(idCategory, idProduct);
            return Ok();
        }
    }
}
