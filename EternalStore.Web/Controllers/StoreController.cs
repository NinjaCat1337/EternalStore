using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreManager storeManager;

        public StoreController(IStoreManager storeManager) => this.storeManager = storeManager;

        // GET: api/Store
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var cat = storeManager.GetAllCategories();
            var name = cat.FirstOrDefault()?.Name;
            //storeManager.CreateCategory("Beer");
            //storeManager.AddProduct(1, "Bud", "Light 0.5L", 24);
            storeManager.EditProduct(1, 1002, "Bud 0.5L", "Light", 20);
            //storeManager.RemoveProduct(1, 1002);
            return new string[] { name };
        }

        // GET: api/Store/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Store
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
