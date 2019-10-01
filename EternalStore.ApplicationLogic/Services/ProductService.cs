using EternalStore.ApplicationLogic.DTO;
using EternalStore.ApplicationLogic.Helpers;
using EternalStore.ApplicationLogic.Interfaces;
using EternalStore.DataAccess.Interfaces;
using EternalStore.DataAccess.Repositories;
using EternalStore.Domain.Models;
using System;
using System.Collections.Generic;

namespace EternalStore.ApplicationLogic.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork Database { get; set; }

        public ProductService(string connectionString) =>
            Database = new UnitOfWork(connectionString);

        public void InsertProduct(ProductDTO productDto) => Database.Products.Insert(ApplicationLogicMapper.FromProductDtoToProduct(productDto));

        public void ModifyProduct(ProductDTO productDto) => Database.Products.Modify(ApplicationLogicMapper.FromProductDtoToProduct(productDto));

        public void EliminateProduct(int id) => Database.Products.Eliminate(id);

        public ProductDTO GetProduct(int id) => ApplicationLogicMapper.FromProductToProductDto(Database.Products.Get(id));

        public IEnumerable<ProductDTO> GetAllProducts() => ApplicationLogicMapper.FromProductsToProductsDto(Database.Products.GetAll());

        public IEnumerable<ProductDTO> GetProductsBy(Func<Product, bool> predicate) =>
            ApplicationLogicMapper.FromProductsToProductsDto(Database.Products.GetBy(predicate));

        public void Dispose() => Database.Dispose();
    }
}
