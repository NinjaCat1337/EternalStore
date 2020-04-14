using EternalStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.Domain.StoreManagement;

namespace EternalStore.DataAccess.StoreManagement.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly StoreDbContext dbContext;
        private bool disposed;

        public OrderRepository(string connectionString) => dbContext = new StoreDbContext(connectionString);

        /// <summary>
        /// Get all orders from database.
        /// </summary>
        /// <returns>IQueryable collection of Orders.</returns>
        public IQueryable<Order> GetAll() => dbContext.Set<Order>()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .AsQueryable();

        /// <summary>
        /// Get by predicate.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> GetByAsync(Func<Order, bool> predicate)
        {
            var allOrders = await dbContext.Orders.ToListAsync();
            return allOrders.Where(predicate);
        }

        /// <summary>
        /// Create Order in database.
        /// </summary>
        /// <param name="order">Order Entity</param>
        public async Task InsertAsync(Order order) => await dbContext.Orders.AddAsync(order);

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
        public async Task<Order> GetAsync(int id)
        {
            var order = await dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

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
