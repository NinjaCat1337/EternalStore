using EternalStore.ApplicationLogic.StoreManagement.DTO;
using EternalStore.Domain.StoreManagement;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.ApplicationLogic.StoreManagement
{
    public static class StoreMapper
    {
        public static IEnumerable<CategoryDTO> FromCategoriesToCategoriesDTO(IEnumerable<Category> categories) =>
            categories.Select(category => new CategoryDTO
            {
                IdCategory = category.Id,
                Name = category.Name,
                IsEnabled = category.IsEnabled
            });

        public static IEnumerable<ProductDTO> FromProductsToProductsDTO(IEnumerable<Product> products) =>
            products.Select(product => new ProductDTO
            {
                IdProduct = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IdCategory = product.Category.Id
            });

        public static ProductDTO FromProductToProductDTO(Product product) =>
            new ProductDTO
            {
                IdProduct = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IdCategory = product.Category.Id
            };

        public static CategoryDTO FromCategoryToCategoryDTO(Category category) =>
            new CategoryDTO
            {
                IdCategory = category.Id,
                Name = category.Name,
                IsEnabled = category.IsEnabled
            };

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
                IdProduct = orderItem.Product.Id,
                ProductName = orderItem.Product.Name
            });
    }
}
