using EternalStore.DataAccess.EntityFramework;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.DataAccess.Repositories
{
    internal class ProductRepository : IRepository<Product>
    {
        private readonly EternalStoreDbContext dbContext;

        public ProductRepository(EternalStoreDbContext dataContext) => this.dbContext = dataContext;

        public IEnumerable<Product> GetAll() => dbContext.Products;

        public IEnumerable<Product> GetBy(Func<Product, bool> predicate) => dbContext.Products.Where(predicate).ToList();

        public void Insert(Product product) => dbContext.Products.Add(product);

        public void Modify(Product product) => dbContext.Entry(product).State = EntityState.Modified;

        public Product Get(int id) => dbContext.Products.Find(id);

        public void Eliminate(int id)
        {
            var product = dbContext.Products.Find(id);

            if (product != null) dbContext.Products.Remove(product);
        }
    }
}
