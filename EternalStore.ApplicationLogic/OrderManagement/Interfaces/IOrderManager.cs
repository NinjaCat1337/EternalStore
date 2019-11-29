using EternalStore.ApplicationLogic.OrderManagement.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.OrderManagement.Interfaces
{
    public interface IOrderManager : IDisposable
    {
        Task<OrderDTO> GetOrderAsync(int idOrder);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task CreateOrderAsync(OrderDTO orderDTO);
        Task ModifyOrderAsync(OrderDTO orderDTO);
        Task SetApprovedAsync(int idOrder);
        Task SetDeliveredAsync(int idOrder);
        Task AddOrderItemAsync(int idOrder, string name, int qty);
        Task RemoveOrderItemAsync(int idOrder, int idOrderItem);
    }
}
