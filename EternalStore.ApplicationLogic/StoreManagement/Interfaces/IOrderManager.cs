using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.StoreManagement.DTO;

namespace EternalStore.ApplicationLogic.StoreManagement.Interfaces
{
    public interface IOrderManager : IDisposable
    {
        Task<OrderDTO> GetOrderAsync(int idOrder);
        Task<(IEnumerable<OrderDTO> OrdersForResponse, int AllOrdersCount)> GetAllOrdersAsync(int? skip, int? take, bool? ascending);

        Task<(IEnumerable<OrderDTO> OrdersForResponse, int FilteredOrdersCount)> SearchOrdersAsync(int? skip, int? take, bool? ascending, DateTime? orderDateFrom,
            DateTime? orderDateTo, DateTime? deliveryDateFrom, DateTime? deliveryDateTo, bool? isApproved, bool? isDelivered);
        Task<int> CreateOrderAsync(OrderDTO orderDTO);
        Task UpdateOrderAsync(OrderDTO orderDTO);
        Task DeleteOrderAsync(int idOrder);
        Task SetApprovedAsync(int idOrder);
        Task SetDeliveredAsync(int idOrder);
        Task AddOrderItemAsync(int idOrder, int idCategory, int idProduct, int qty);
        Task RemoveOrderItemAsync(int idOrder, int idOrderItem);
    }
}
