using EternalStore.DataAccess.EntityFramework;
using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.OrderManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.DataAccess.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly EternalStoreDbContext dbContext;

        public OrderRepository(EternalStoreDbContext dbContext) => this.dbContext = dbContext;

        public IEnumerable<Order> GetAll() => dbContext.Orders;

        public IEnumerable<Order> GetBy(Func<Order, bool> predicate) => dbContext.Orders.Where(predicate).ToList();

        public void Insert(Order order) => dbContext.Orders.Add(order);

        public void Modify(Order order) => dbContext.Entry(order).State = EntityState.Modified;

        public Order Get(int id) => dbContext.Orders.Find(id);

        public void Eliminate(int id)
        {
            var order = dbContext.Orders.Find(id);

            if (order != null) dbContext.Orders.Remove(order);
        }
    }
}
