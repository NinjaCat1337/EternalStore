using EternalStore.ApplicationLogic.StoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.StoreManagement.Interfaces
{
    public interface IStoreManager : IDisposable
    {
        Task CreateCategory(string name);
        Task UpdateCategory(int idCategory, string name);
        Task DisableCategory(int idCategory);
        Task EnableCategory(int idCategory);
        Task AddProduct(int idCategory, string name, string description, decimal price);
        Task EditProduct(int idCategory, int idProduct, string name, string description, decimal price);
        Task RemoveProduct(int idCategory, int idProduct);
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<IEnumerable<ProductDTO>> GetProductsForCategory(int idCategory);
        Task<ProductDTO> GetProduct(int idCategory, int idProduct);
    }
}
