using EternalStore.DataAccess.Interfaces;
using EternalStore.Domain.OrderManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.DataAccess.OrderManagement.Repositories
{
    public class OrderRepository : IRepository<Order>, IDisposable
    {
        private readonly OrdersDbContext dbContext;
        private bool disposed;

        public OrderRepository(string connectionString) => dbContext = new OrdersDbContext(connectionString);

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

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

        private void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) dbContext.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OrderRepository() => Dispose(false);
    }
}
