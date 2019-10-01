using EternalStore.ApplicationLogic.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.Interfaces
{
    public interface IProductService
    {
        Task InsertProduct(ProductDTO productDto);
        Task ModifyProduct(ProductDTO productDto);
        Task EliminateProduct(int id);
        ProductDTO GetProduct(int id);
        IEnumerable<ProductDTO> GetAllProducts();
        void Dispose();
    }
}
