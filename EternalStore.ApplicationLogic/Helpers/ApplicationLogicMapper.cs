using EternalStore.ApplicationLogic.DTO;
using EternalStore.Domain.OrderManagement;
using EternalStore.Domain.ProductManagement;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.ApplicationLogic.Helpers
{
    public static class ApplicationLogicMapper
    {
        #region Product

        public static Product FromProductDtoToProduct(ProductDTO productDto) =>
            new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

        public static ProductDTO FromProductToProductDto(Product product) =>
            new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

        public static IEnumerable<Product> FromProductsDtoToProducts(IEnumerable<ProductDTO> productsDto) =>
            productsDto.Select(productDto => new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            }).ToList();

        public static IEnumerable<ProductDTO> FromProductsToProductsDto(IEnumerable<Product> products) =>
            products.Select(product => new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            }).ToList();


        #endregion

        #region Category

        public static Category FromCategoryDtoToCategory(CategoryDTO categoryDto) =>
            new Category
            {
                Name = categoryDto.Name,
                CategoryId = categoryDto.CategoryId
            };

        public static CategoryDTO FromCategoryToCategoryDto(Category category) =>
            new CategoryDTO
            {
                Name = category.Name,
                CategoryId = category.CategoryId
            };

        public static IEnumerable<Category> FromCategoriesDtoToCategories(IEnumerable<CategoryDTO> categoriesDto) =>
            categoriesDto.Select(categoryDto => new Category
            {
                Name = categoryDto.Name,
                CategoryId = categoryDto.CategoryId
            }).ToList();

        public static IEnumerable<CategoryDTO> FromCategoriesToCategoriesDto(IEnumerable<Category> categories) =>
            categories.Select(category => new CategoryDTO
            {
                Name = category.Name,
                CategoryId = category.CategoryId
            }).ToList();

        #endregion

        #region Order

        public static Order FromOrderDtoToOrder(OrderDTO orderDto) =>
            new Order
            {
                OrderId = orderDto.OrderId,
                AdditionalInformation = orderDto.AdditionalInformation,
                CustomerAddress = orderDto.CustomerAddress,
                CustomerNumber = orderDto.CustomerNumber,
                DeliveryDate = orderDto.DeliveryDate,
                IsApproved = orderDto.IsApproved,
                IsDelivered = orderDto.IsDelivered,
                OrderDate = orderDto.OrderDate,
                OrderItems = orderDto.OrderItems
            };

        public static OrderDTO FromOrderToOrderDto(Order order) =>
            new OrderDTO
            {
                OrderId = order.OrderId,
                AdditionalInformation = order.AdditionalInformation,
                CustomerAddress = order.CustomerAddress,
                CustomerNumber = order.CustomerNumber,
                DeliveryDate = order.DeliveryDate,
                IsApproved = order.IsApproved,
                IsDelivered = order.IsDelivered,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems
            };

        public static IEnumerable<Order> FromOrdersDtoToOrders(IEnumerable<OrderDTO> ordersDto) =>
            ordersDto.Select(orderDto => new Order
            {
                OrderId = orderDto.OrderId,
                AdditionalInformation = orderDto.AdditionalInformation,
                CustomerAddress = orderDto.CustomerAddress,
                CustomerNumber = orderDto.CustomerNumber,
                DeliveryDate = orderDto.DeliveryDate,
                IsApproved = orderDto.IsApproved,
                IsDelivered = orderDto.IsDelivered,
                OrderDate = orderDto.OrderDate,
                OrderItems = orderDto.OrderItems
            }).ToList();

        public static IEnumerable<OrderDTO> FromOrdersToOrdersDto(IEnumerable<Order> orders) =>
            orders.Select(order => new OrderDTO
            {
                OrderId = order.OrderId,
                AdditionalInformation = order.AdditionalInformation,
                CustomerAddress = order.CustomerAddress,
                CustomerNumber = order.CustomerNumber,
                DeliveryDate = order.DeliveryDate,
                IsApproved = order.IsApproved,
                IsDelivered = order.IsDelivered,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems
            }).ToList();

        #endregion
    }
}
