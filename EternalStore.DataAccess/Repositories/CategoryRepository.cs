using EternalStore.DataAccess.EntityFramework;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.DataAccess.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly EternalStoreDbContext dbContext;

        public CategoryRepository(EternalStoreDbContext dbContext) => this.dbContext = dbContext;

        public IEnumerable<Category> GetAll() => dbContext.Categories;

        public IEnumerable<Category> GetBy(Func<Category, bool> predicate) => dbContext.Categories.Where(predicate).ToList();

        public void Insert(Category category) => dbContext.Categories.Add(category);

        public void Modify(Category category) => dbContext.Entry(category).State = EntityState.Modified;

        public Category Get(int id) => dbContext.Categories.Find(id);

        public void Eliminate(int id)
        {
            var category = dbContext.Categories.Find(id);

            if (category != null) dbContext.Categories.Remove(category);
        }
    }
}
