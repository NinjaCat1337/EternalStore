using EternalStore.ApplicationLogic.StoreManagement.DTO;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.DataAccess.StoreManagement.Repositories;
using EternalStore.Domain.StoreManagement;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CategoryDTO> GetCategoryAsync(int idCategory)
        {
            var category = await storeRepository.GetAsync(idCategory);
            return StoreMapper.FromCategoryToCategoryDTO(category);
        }

        public async Task<int> CreateCategoryAsync(string name)
        {
            var category = await storeRepository.GetByAsync(c => c.Name == name);
            if (category.Any())
                throw new Exception("Category with same name already exists.");

            var newCategory = Category.Insert(name);
            await storeRepository.InsertAsync(newCategory);

            await storeRepository.SaveChangesAsync();

            return newCategory.Id;
        }

        public async Task UpdateCategoryAsync(int id, string name)
        {
            var category = await storeRepository.GetAsync(id);
            category.Modify(name);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task DisableCategoryAsync(int id)
        {
            var category = await storeRepository.GetAsync(id);
            category.Disable();
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task EnableCategoryAsync(int id)
        {
            var category = await storeRepository.GetAsync(id);
            category.Enable();
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task<int> AddProductAsync(int idCategory, string name, string description, decimal price)
        {
            var category = await storeRepository.GetAsync(idCategory);
            var product = category.AddProduct(name, description, price);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();

            return product.Id;
        }

        public async Task EditProductAsync(int idCategory, int idProduct, string name, string description, decimal price)
        {
            var category = await storeRepository.GetAsync(idCategory);
            category.EditProduct(idProduct, name, description, price);
            var product = category.Products.FirstOrDefault(p => p.Id == idProduct);
            storeRepository.Modify(product);

            await storeRepository.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(int idCategory, int idProduct)
        {
            var category = await storeRepository.GetAsync(idCategory);
            var product = category.Products.FirstOrDefault(p => p.Id == idProduct);

            if (product == null)
                throw new Exception("Product not found.");

            storeRepository.Eliminate(product);

            await storeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categoriesQuery = storeRepository.GetAll();
            var categories = await categoriesQuery.ToListAsync();
            return StoreMapper.FromCategoriesToCategoriesDTO(categories);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsForCategoryAsync(int idCategory)
        {
            var category = await storeRepository.GetAsync(idCategory);
            return StoreMapper.FromProductsToProductsDTO(category.Products);
        }

        public async Task<ProductDTO> GetProductAsync(int idCategory, int idProduct)
        {
            var category = await storeRepository.GetAsync(idCategory);
            var product = category.Products.FirstOrDefault(p => p.Id == idProduct);

            if (product == null)
                throw new Exception("Product not found.");

            return StoreMapper.FromProductToProductDTO(product);
        }

        public void Dispose() => storeRepository.Dispose();
    }
}
