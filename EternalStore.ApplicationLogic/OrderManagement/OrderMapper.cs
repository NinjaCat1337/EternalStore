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
                IsDelivered = order.IsDelivered,
                OrderItems = FromOrderItemsToOrderItemsDTO(order.OrderItems).ToList()
            }).ToList();

        public static IEnumerable<OrderItemDTO> FromOrderItemsToOrderItemsDTO(IEnumerable<OrderItem> orderItems) =>
            orderItems.Select(orderItem => new OrderItemDTO
            {
                Id = orderItem.Id,
                Qty = orderItem.Qty,
                IdProduct = orderItem.Product.Id
            });
    }
}
