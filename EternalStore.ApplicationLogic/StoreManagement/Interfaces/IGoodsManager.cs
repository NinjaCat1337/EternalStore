using EternalStore.ApplicationLogic.StoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.StoreManagement.Interfaces
{
    public interface IGoodsManager : IDisposable
    {
        Task<CategoryDTO> GetCategoryAsync(int idCategory);
        Task<int> CreateCategoryAsync(string name);
        Task UpdateCategoryAsync(int idCategory, string name);
        Task DisableCategoryAsync(int idCategory);
        Task EnableCategoryAsync(int idCategory);
        Task<int> AddProductAsync(int idCategory, string name, string description, decimal price);
        Task EditProductAsync(int idCategory, int idProduct, string name, string description, decimal price);
        Task RemoveProductAsync(int idCategory, int idProduct);
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<IEnumerable<ProductDTO>> GetProductsForCategoryAsync(int idCategory);
        Task<ProductDTO> GetProductAsync(int idCategory, int idProduct);
    }
}
