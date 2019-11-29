using EternalStore.ApplicationLogic.OrderManagement.DTO;
using EternalStore.ApplicationLogic.OrderManagement.Interfaces;
using EternalStore.DataAccess.OrderManagement.Repositories;
using EternalStore.Domain.OrderManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.OrderManagement
{
    public class OrderManager : IOrderManager
    {
        private readonly OrderRepository orderRepository;

        public OrderManager(string connectionString) => orderRepository ??= new OrderRepository(connectionString);

        public async Task<OrderDTO> GetOrderAsync(int idOrder)
        {
            var order = await orderRepository.Get(idOrder);

            return OrderMapper.FromOrderToOrderDTO(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetAll();

            return OrderMapper.FromOrdersToOrdersDTO(orders);
        }

        public async Task CreateOrderAsync(OrderDTO orderDTO)
        {
            var order = Order.Insert(orderDTO.DeliveryDate, orderDTO.CustomerNumber, orderDTO.CustomerAddress, orderDTO.AdditionalInformation);
            await orderRepository.Insert(order);

            //foreach (var orderItem in orderDTO.OrderItems)
            //{
            //    order.AddOrderItem(orderItem.Name, orderItem.Qty);
            //}

            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task ModifyOrderAsync(OrderDTO orderDTO)
        {
            var order = await orderRepository.Get(orderDTO.Id);
            order.Modify(orderDTO.DeliveryDate, orderDTO.CustomerAddress, orderDTO.CustomerNumber, orderDTO.AdditionalInformation);
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetApprovedAsync(int id)
        {
            var order = await orderRepository.Get(id);
            order.SetApproved();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task SetDeliveredAsync(int id)
        {
            var order = await orderRepository.Get(id);
            order.SetDelivered();
            orderRepository.Modify(order);

            await orderRepository.SaveChangesAsync();
        }

        public async Task AddOrderItemAsync(int idOrder, string name, int qty)
        {
            var category = await orderRepository.Get(idOrder);
            //category.AddOrderItem(name, qty);
            orderRepository.Modify(category);

            await orderRepository.SaveChangesAsync();
        }

        public async Task RemoveOrderItemAsync(int idOrder, int idOrderItem)
        {
            var order = await orderRepository.Get(idOrder);
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == idOrderItem);
            orderRepository.Eliminate(orderItem);

            await orderRepository.SaveChangesAsync();
        }

        public void Dispose() => orderRepository.Dispose();
    }
}
