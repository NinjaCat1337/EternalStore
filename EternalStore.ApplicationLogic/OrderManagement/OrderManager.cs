using EternalStore.ApplicationLogic.OrderManagement.DTO;
using EternalStore.ApplicationLogic.OrderManagement.Interfaces;
using EternalStore.DataAccess.OrderManagement.Repositories;
using EternalStore.Domain.OrderManagement;
using System;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.OrderManagement
{
    public class OrderManager : IOrderManager
    {
        private readonly OrderRepository orderRepository;

        public OrderManager(string connectionString) => orderRepository ??= new OrderRepository(connectionString);

        private Order GetOrder(int id)
        {
            var order = orderRepository.Get(id);

            if (order == null)
                throw new Exception("Order not found.");

            return order;
        }

        public async Task CreateOrder(OrderDTO orderDTO)
        {
            var order = Order.Insert(orderDTO.DeliveryDate, orderDTO.CustomerNumber, orderDTO.CustomerAddress, orderDTO.AdditionalInformation);
            orderRepository.Insert(order);

            foreach (var orderItem in orderDTO.OrderItems) { order.AddOrderItem(orderItem.Name, orderItem.Qty); }

            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task ChangeAddress(int id, string address)
        {
            var order = GetOrder(id);
            order.ChangeAddress(address);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task ChangeNumber(int id, string number)
        {
            var order = GetOrder(id);
            order.ChangeNumber(number);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task ChangeDeliveryDate(int id, DateTime deliveryDate)
        {
            var order = GetOrder(id);
            order.ChangeDeliveryDate(deliveryDate);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetApproved(int id)
        {
            var order = GetOrder(id);
            order.SetApproved();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetDelivered(int id)
        {
            var order = GetOrder(id);
            order.SetDelivered();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task RemoveOrderItem(int orderId, int orderItemId)
        {
            var order = GetOrder(orderId);
            order.RemoveOrderItem(orderItemId);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public void Dispose() => orderRepository.Dispose();
    }
}
