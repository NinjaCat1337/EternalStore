using EternalStore.ApplicationLogic.DTO;
using EternalStore.ApplicationLogic.Helpers;
using EternalStore.ApplicationLogic.Interfaces;
using EternalStore.DataAccess.Interfaces;
using EternalStore.DataAccess.Repositories;
using EternalStore.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork Database { get; set; }

        public ProductService(string connectionString) => Database = new UnitOfWork(connectionString);

        public async Task InsertProduct(ProductDTO productDto)
        {
            Database.Products.Insert(ApplicationLogicMapper.FromProductDtoToProduct(productDto));
            await Database.SaveAsync();
        }

        public async Task ModifyProduct(ProductDTO productDto)
        {
            if (Database.Products.Get(productDto.ProductId) == null)
                throw new ValidationException("Item not found.", "Product");

            Database.Products.Modify(ApplicationLogicMapper.FromProductDtoToProduct(productDto));
            await Database.SaveAsync();
        }

        public async Task EliminateProduct(int id)
        {
            if (Database.Products.Get(id) == null)
                throw new ValidationException("Item not found.", "Product");

            Database.Products.Eliminate(id);
            await Database.SaveAsync();
        }

        public ProductDTO GetProduct(int id)
        {
            var product = Database.Products.Get(id);
            if (product == null)
                throw new ValidationException("Item not found.", "Product");

            return ApplicationLogicMapper.FromProductToProductDto(product);
        }

        public IEnumerable<ProductDTO> GetAllProducts() => ApplicationLogicMapper.FromProductsToProductsDto(Database.Products.GetAll());

        public void Dispose() => Database.Dispose();
    }
}
