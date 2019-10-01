using EternalStore.ApplicationLogic.DTO;
using EternalStore.Domain.Models;
using System;
using System.Collections.Generic;

namespace EternalStore.ApplicationLogic.Interfaces
{
    public interface IProductService
    {
        void InsertProduct(ProductDTO productDto);
        void ModifyProduct(ProductDTO productDto);
        void EliminateProduct(int id);
        ProductDTO GetProduct(int id);
        IEnumerable<ProductDTO> GetAllProducts();
        IEnumerable<ProductDTO> GetProductsBy(Func<Product, bool> predicate);
        void Dispose();
    }
}
