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

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>IEnumerable collection of Orders.</returns>
        public async Task<IEnumerable<Order>> GetAll() => await dbContext.Orders.ToListAsync();

        /// <summary>
        /// Get by predicate.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetBy(Func<Order, bool> predicate) =>
            await dbContext.Orders.Where(predicate).AsQueryable().ToListAsync();

        /// <summary>
        /// Create Order in database.
        /// </summary>
        /// <param name="order">Order Entity</param>
        public async Task Insert(Order order) => await dbContext.Orders.AddAsync(order);

        /// <summary>
        /// Update Order in database.
        /// </summary>
        /// <param name="item">Should be an Order type.</param>
        public void Modify(object item)
        {
            if (item as Order != null)
                dbContext.Entry(item).State = EntityState.Modified;

            else
                throw new Exception("Wrong type.");
        }

        /// <summary>
        /// Get Order from database by Id.
        /// </summary>
        /// <param name="id">Id Order</param>
        /// <returns></returns>
        public async Task<Order> Get(int id)
        {
            var order = await dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new Exception("Order not found.");

            return order;
        }

        /// <summary>
        /// Delete Order or OrderItem from database.
        /// </summary>
        /// <param name="item">Should be an Order or OrderItem type.</param>
        public void Eliminate(object item)
        {
            if (item as OrderItem != null || item as Order != null)
                dbContext.Entry(item).State = EntityState.Modified;

            else
                throw new Exception("Wrong type.");
        }

        /// <summary>
        /// Save changes in database.
        /// </summary>
        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

        /// <summary>
        /// Close connection with database.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) dbContext.Dispose();

            disposed = true;
        }

        ~OrderRepository() => Dispose(false);
    }
}
