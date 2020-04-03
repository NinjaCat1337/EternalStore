using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.StoreManagement.DTO;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.DataAccess.StoreManagement.Repositories;
using EternalStore.Domain.StoreManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EternalStore.ApplicationLogic.StoreManagement
{
    public class StatisticManager : IStatisticManager
    {
        private readonly GoodsRepository storeRepository;
        private readonly OrderRepository orderRepository;

        public StatisticManager(IConfiguration configuration)
        {
            storeRepository ??= new GoodsRepository(configuration.GetConnectionString("DefaultConnection"));
            orderRepository ??= new OrderRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<ProductStatisticDTO>> GetProductsStatistic(DateTime dateFrom, DateTime dateTo)
        {
            var ordersByDate = await orderRepository.GetAll()
                .Where(o => o.OrderDate >= dateFrom && o.OrderDate <= dateTo).ToListAsync();
            var allCategories = await storeRepository.GetAll().ToListAsync();

            var allOrderItems = new List<OrderItem>();
            var allProductsStatistic = (from category in allCategories
                                        from product in category.Products
                                        select new ProductStatisticDTO { IdProduct = product.Id, ProductName = product.Name }).ToList();

            foreach (var order in ordersByDate)
                allOrderItems.AddRange(order.OrderItems);

            foreach (var productStatistic in allProductsStatistic)
                foreach (var orderItem in allOrderItems.Where(orderItem => orderItem.Product.Id == productStatistic.IdProduct))
                    productStatistic.TotalQty += orderItem.Qty;

            return allProductsStatistic;
        }
    }
}
