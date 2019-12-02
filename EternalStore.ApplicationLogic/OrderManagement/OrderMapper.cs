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
                IdOrder = order.Id,
                CustomerName = order.CustomerName,
                AdditionalInformation = order.AdditionalInformation,
                CustomerAddress = order.CustomerAddress,
                CustomerNumber = order.CustomerNumber,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                IsApproved = order.IsApproved,
                IsDelivered = order.IsDelivered,
                OrderItems = FromOrderItemsToOrderItemsDTO(order.OrderItems).ToList()
            };

        public static IEnumerable<OrderDTO> FromOrdersToOrdersDTO(IEnumerable<Order> orders) =>
            orders.Select(order => new OrderDTO
            {
                IdOrder = order.Id,
                CustomerName = order.CustomerName,
                AdditionalInformation = order.AdditionalInformation,
                CustomerAddress = order.CustomerAddress,
                CustomerNumber = order.CustomerNumber,
                DeliveryDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                IsApproved = order.IsApproved,
                IsDelivered = order.IsDelivered,
                OrderItems = FromOrderItemsToOrderItemsDTO(order.OrderItems).ToList()
            });

        public static IEnumerable<OrderItemDTO> FromOrderItemsToOrderItemsDTO(IEnumerable<OrderItem> orderItems) =>
            orderItems.Select(orderItem => new OrderItemDTO
            {
                IdOrderItem = orderItem.Id,
                Qty = orderItem.Qty,
                IdProduct = orderItem.Product.Id
            });
    }
}
