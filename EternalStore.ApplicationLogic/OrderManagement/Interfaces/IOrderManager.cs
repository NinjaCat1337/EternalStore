using EternalStore.ApplicationLogic.OrderManagement.DTO;
using System;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.OrderManagement.Interfaces
{
    public interface IOrderManager : IDisposable
    {
        Task CreateOrder(OrderDTO orderDTO);
        Task ModifyOrder(OrderDTO orderDTO);
        Task SetApproved(int id);
        Task SetDelivered(int id);
        Task AddOrderItem(int idOrder, string name, int qty);
        Task RemoveOrderItem(int idOrder, int idOrderItem);
    }
}
