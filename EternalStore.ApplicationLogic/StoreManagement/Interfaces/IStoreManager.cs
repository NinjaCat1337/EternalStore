using System;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.StoreManagement.Interfaces
{
    public interface IStoreManager : IDisposable
    {
        Task CreateCategory(string name);
        Task UpdateCategory(int id, string name);
        Task DisableCategory(int id);
        Task EnableCategory(int id);
        Task AddProduct(int categoryId, string name, string description, decimal price);
        Task EditProduct(int categoryId, int productId, string name, string description, decimal price);
        Task RemoveProduct(int categoryId, int productId);
    }
}
