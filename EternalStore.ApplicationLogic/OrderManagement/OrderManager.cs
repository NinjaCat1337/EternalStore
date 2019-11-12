using EternalStore.ApplicationLogic.OrderManagement.DTO;
using EternalStore.ApplicationLogic.OrderManagement.Interfaces;
using EternalStore.DataAccess.OrderManagement.Repositories;
using EternalStore.Domain.OrderManagement;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.OrderManagement
{
    public class OrderManager : IOrderManager
    {
        private readonly OrderRepository orderRepository;

        public OrderManager(string connectionString) => orderRepository ??= new OrderRepository(connectionString);

        public async Task CreateOrder(OrderDTO orderDTO)
        {
            var order = Order.Insert(orderDTO.DeliveryDate, orderDTO.CustomerNumber, orderDTO.CustomerAddress, orderDTO.AdditionalInformation);
            orderRepository.Insert(order);

            foreach (var orderItem in orderDTO.OrderItems) { order.AddOrderItem(orderItem.Name, orderItem.Qty); }

            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task ModifyOrder(OrderDTO orderDTO)
        {
            var order = orderRepository.Get(orderDTO.Id);
            order.Modify(orderDTO.DeliveryDate, orderDTO.CustomerAddress, orderDTO.CustomerNumber, orderDTO.AdditionalInformation);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetApproved(int id)
        {
            var order = orderRepository.Get(id);
            order.SetApproved();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetDelivered(int id)
        {
            var order = orderRepository.Get(id);
            order.SetDelivered();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task AddOrderItem(int idOrder, string name, int qty)
        {
            var category = orderRepository.Get(idOrder);
            category.AddOrderItem(name, qty);
            orderRepository.Modify(category);

            await orderRepository.SaveChangesAsync();
        }

        public async Task RemoveOrderItem(int idOrder, int idOrderItem)
        {
            var order = orderRepository.Get(idOrder);
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == idOrderItem);
            orderRepository.Eliminate(orderItem);

            await orderRepository.SaveChangesAsync();
        }

        public void Dispose() => orderRepository.Dispose();
    }
}
