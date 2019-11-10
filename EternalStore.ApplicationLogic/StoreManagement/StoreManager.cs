using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.DataAccess.StoreManagement.Repositories;
using EternalStore.Domain.StoreManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.StoreManagement
{
    public class StoreManager : IStoreManager
    {
        private readonly StoreRepository storeRepository;

        public StoreManager(string connectionString) => storeRepository ??= new StoreRepository(connectionString);

        private Category GetCategory(int id)
        {
            var category = storeRepository.Get(id);

            if (category == null)
                throw new Exception("Category not found.");

            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return storeRepository.GetAll();
        }

        public async Task CreateCategory(string name)
        {
            storeRepository.Insert(Category.Insert(name));

            await storeRepository.SaveChangesAsync();
        }

        public async Task UpdateCategory(int id, string name)
        {
            var category = GetCategory(id);
            category.Modify(name);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task DisableCategory(int id)
        {
            var category = GetCategory(id);
            category.Disable();
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task EnableCategory(int id)
        {
            var category = GetCategory(id);
            category.Enable();
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task AddProduct(int categoryId, string name, string description, decimal price)
        {
            var category = GetCategory(categoryId);
            category.AddProduct(name, description, price);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task EditProduct(int categoryId, int productId, string name, string description, decimal price)
        {
            var category = GetCategory(categoryId);
            category.EditProduct(productId, name, description, price);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public async Task RemoveProduct(int categoryId, int productId)
        {
            var category = GetCategory(categoryId);
            category.RemoveProduct(productId);
            storeRepository.Modify(category);

            await storeRepository.SaveChangesAsync();
        }

        public void Dispose() => storeRepository.Dispose();
    }
}
