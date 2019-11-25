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

        // GET: api/Store
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    var cat = storeManager.GetCategories();
        //    var name = cat.FirstOrDefault()?.Name;
        //    //storeManager.CreateCategory("Beer");
        //    //storeManager.AddProduct(1, "Bud", "Light 0.5L", 24);
        //    //storeManager.EditProduct(1, 1002, "Bud 0.5L", "Light", 20);
        //    //storeManager.RemoveProduct(1, 1002);
        //    return new string[] { name };
        //}

        //[HttpGet]
        //public IEnumerable<CategoryDTO> Get()
        //{
        //    return storeManager.GetCategories();
        //}

        [HttpGet("categories", Name = "GetCategories")]
        public async Task<IActionResult> Get()
        {
            var categories = await storeManager.GetCategories();
            return Ok(categories);
        }

        [HttpGet("categories/{idCategory}/products", Name = "GetProductsForCategory")]
        public async Task<IActionResult> Get(int idCategory)
        {
            var productsForCategory = await storeManager.GetProductsForCategory(idCategory);
            return Ok(productsForCategory);
        }

        [HttpGet("categories/{idCategory}/products/{idProduct}", Name = "GetProduct")]
        public async Task<IActionResult> Get(int idCategory, int idProduct)
        {
            var product = await storeManager.GetProduct(idCategory, idProduct);
            return Ok(product);
        }
    }
}
