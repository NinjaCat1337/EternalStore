using EternalStore.ApplicationLogic.StoreManagement.DTO;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.DataAccess.StoreManagement.Repositories;
using EternalStore.Domain.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.StoreManagement
{
    public class StoreManager : IStoreManager
    {
        private readonly StoreRepository storeRepository;

        public StoreManager(string connectionString) =>
            storeRepository ??= new StoreRepository(connectionString);

        public async Task CreateCategory(string name)
        {
            await storeRepository.Insert(Category.Insert(name));

            await storeRepository.SaveChangesAsync();
        }

        public async Task UpdateCategory(int id, string name)
        {
            var category = await storeRepository.Get(id);
            category.Modify(name);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task DisableCategory(int id)
        {
            var category = await storeRepository.Get(id);
            category.Disable();
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task EnableCategory(int id)
        {
            var category = await storeRepository.Get(id);
            category.Enable();
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task AddProduct(int idCategory, string name, string description, decimal price)
        {
            var category = await storeRepository.Get(idCategory);
            category.AddProduct(name, description, price);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task EditProduct(int idCategory, int idProduct, string name, string description, decimal price)
        {
            var category = await storeRepository.Get(idCategory);
            category.EditProduct(idProduct, name, description, price);
            var product = category.Products.FirstOrDefault(p => p.Id == idProduct);
            storeRepository.Modify(product);

            await storeRepository.SaveChangesAsync();
        }

        public async Task RemoveProduct(int idCategory, int idProduct)
        {
            var category = await storeRepository.Get(idCategory);
            var product = category.Products.FirstOrDefault(p => p.Id == idProduct);

            if (product != null)
                storeRepository.Eliminate(product);

            await storeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await storeRepository.GetAll();
            return StoreMapper.FromCategoriesToCategoriesDTO(categories);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsForCategory(int idCategory)
        {
            var category = await storeRepository.Get(idCategory);
            return StoreMapper.FromProductsToProductsDTO(category.Products);
        }

        public async Task<ProductDTO> GetProduct(int idCategory, int idProduct)
        {
            var category = await storeRepository.Get(idCategory);
            var product = category.Products.FirstOrDefault(p => p.Id == idProduct);

            if (product == null)
                throw new Exception("Product not found.");

            return StoreMapper.FromProductToProductDTO(product);
        }

        public void Dispose() => storeRepository.Dispose();
    }
}
