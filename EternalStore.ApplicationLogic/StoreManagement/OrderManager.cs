using EternalStore.DataAccess.StoreManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.StoreManagement.DTO;
using EternalStore.ApplicationLogic.StoreManagement.Interfaces;
using EternalStore.Domain.StoreManagement;

namespace EternalStore.ApplicationLogic.StoreManagement
{
    public class OrderManager : IOrderManager
    {
        private readonly OrderRepository orderRepository;
        private readonly GoodsRepository storeRepository;

        public OrderManager(string connectionString)
        {
            orderRepository ??= new OrderRepository(connectionString);
            storeRepository ??= new GoodsRepository(connectionString);
        }

        public async Task<OrderDTO> GetOrderAsync(int idOrder)
        {
            var order = await orderRepository.GetAsync(idOrder);

            return StoreMapper.FromOrderToOrderDTO(order);
        }

        public async Task<(IEnumerable<OrderDTO> OrdersForResponse, int FilteredOrdersCount)> GetOrdersAsync(int? skip = null, int? take = null, bool? ascending = null, DateTime? orderDateFrom = null,
            DateTime? orderDateTo = null, DateTime? deliveryDateFrom = null, DateTime? deliveryDateTo = null, bool? isApproved = null, bool? isDelivered = null)
        {
            var query = orderRepository.GetAll();

            if (ascending != null)
                query = (bool)ascending ? query.OrderBy(o => o.Id) : query.OrderByDescending(o => o.Id);

            if (orderDateFrom != null)
                query = query.Where(o => o.OrderDate >= orderDateFrom);

            if (orderDateTo != null)
                query = query.Where(o => o.OrderDate <= orderDateTo);

            if (deliveryDateFrom != null)
                query = query.Where(o => o.DeliveryDate >= deliveryDateFrom);

            if (deliveryDateTo != null)
                query = query.Where(o => o.DeliveryDate <= deliveryDateTo);

            if (isApproved != null)
                query = query.Where(o => o.IsApproved == isApproved);

            if (isDelivered != null)
                query = query.Where(o => o.IsDelivered == isDelivered);

            var filteredOrdersCount = query.Count();

            if (skip != null)
                query = query.Skip(skip.Value);

            if (take != null)
                query = query.Take(take.Value);

            var orders = await query.ToListAsync();

            return (StoreMapper.FromOrdersToOrdersDTO(orders), filteredOrdersCount);
        }

        public async Task<int> CreateOrderAsync(OrderDTO orderDTO)
        {
            var order = Order.Insert(orderDTO.DeliveryDate, orderDTO.CustomerName, orderDTO.CustomerNumber, orderDTO.CustomerAddress, orderDTO.AdditionalInformation);
            await orderRepository.InsertAsync(order);
            await orderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task UpdateOrderAsync(OrderDTO orderDTO)
        {
            var order = await orderRepository.GetAsync(orderDTO.IdOrder);
            order.Modify(orderDTO.DeliveryDate, orderDTO.CustomerName, orderDTO.CustomerAddress, orderDTO.CustomerNumber, orderDTO.AdditionalInformation);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int idOrder)
        {
            var order = await orderRepository.GetAsync(idOrder);
            orderRepository.Eliminate(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetApprovedAsync(int id)
        {
            var order = await orderRepository.GetAsync(id);
            order.SetApproved();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetDeliveredAsync(int id)
        {
            var order = await orderRepository.GetAsync(id);
            order.SetDelivered();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task AddOrderItemAsync(int idOrder, int idCategory, int idProduct, int qty)
        {
            var order = await orderRepository.GetAsync(idOrder);
            var category = await storeRepository.GetAsync(idCategory);
            var product = category.Products.FirstOrDefault(p => p.Id == idProduct);
            order.AddOrderItem(product, qty);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task RemoveOrderItemAsync(int idOrder, int idOrderItem)
        {
            var order = await orderRepository.GetAsync(idOrder);
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == idOrderItem);
            orderRepository.Eliminate(orderItem);

            await orderRepository.SaveChangesAsync();
        }

        public void Dispose() => orderRepository.Dispose();
    }
}
