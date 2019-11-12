using EternalStore.Domain.StoreManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.StoreManagement.Interfaces
{
    public interface IStoreManager : IDisposable
    {
        IEnumerable<Category> GetAllCategories();
        Task CreateCategory(string name);
        Task UpdateCategory(int id, string name);
        Task DisableCategory(int id);
        Task EnableCategory(int id);
        Task AddProduct(int idCategory, string name, string description, decimal price);
        Task EditProduct(int idCategory, int idProduct, string name, string description, decimal price);
        Task RemoveProduct(int idCategory, int idProduct);
    }
}
