using EternalStore.ApplicationLogic.OrderManagement.DTO;
using System;
using System.Threading.Tasks;

namespace EternalStore.ApplicationLogic.OrderManagement.Interfaces
{
    public interface IOrderManager : IDisposable
    {
        Task CreateOrder(OrderDTO orderDTO);
        Task ChangeAddress(int id, string address);
        Task ChangeNumber(int id, string number);
        Task ChangeDeliveryDate(int id, DateTime deliveryDate);
        Task SetApproved(int id);
        Task SetDelivered(int id);
        Task RemoveOrderItem(int orderId, int orderItemId);
    }
}
