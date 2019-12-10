using EternalStore.ApplicationLogic.OrderManagement.DTO;
using EternalStore.ApplicationLogic.OrderManagement.Interfaces;
using EternalStore.DataAccess.OrderManagement.Repositories;
using EternalStore.DataAccess.StoreManagement.Repositories;
using EternalStore.Domain.OrderManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.OrderManagement
{
    public class OrderManager : IOrderManager
    {
        private readonly OrderRepository orderRepository;
        private readonly StoreRepository storeRepository;

        public OrderManager(string connectionString)
        {
            orderRepository ??= new OrderRepository(connectionString);
            storeRepository ??= new StoreRepository(connectionString);
        }

        public async Task<OrderDTO> GetOrderAsync(int idOrder)
        {
            var order = await orderRepository.GetAsync(idOrder);

            return OrderMapper.FromOrderToOrderDTO(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync(int? skip, int? take, bool? ascending)
        {
            var orders = await orderRepository.GetAllAsync(skip, take, ascending);

            return OrderMapper.FromOrdersToOrdersDTO(orders);
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
