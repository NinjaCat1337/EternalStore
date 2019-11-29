using EternalStore.ApplicationLogic.OrderManagement.DTO;
using EternalStore.Domain.OrderManagement;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.ApplicationLogic.OrderManagement
{
    public static class OrderMapper
    {
        public static OrderDTO FromOrderToOrderDTO(Order order) =>
            new OrderDTO
            {
                Id = order.Id,
                AdditionalInformation = order.AdditionalInformation,
                CustomerAddress = order.CustomerAddress,
                CustomerNumber = order.CustomerNumber,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                IsApproved = order.IsApproved,
                IsDelivered = order.IsDelivered
            };

        public static IEnumerable<OrderDTO> FromOrdersToOrdersDTO(IEnumerable<Order> orders) =>
            orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                AdditionalInformation = order.AdditionalInformation,
                CustomerAddress = order.CustomerAddress,
                CustomerNumber = order.CustomerNumber,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                IsApproved = order.IsApproved,
                IsDelivered = order.IsDelivered
            }).ToList();
    }
}
