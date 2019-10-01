using EternalStore.ApplicationLogic.DTO;
using EternalStore.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.ApplicationLogic.Helpers
{
    public static class ApplicationLogicMapper
    {
        public static Product FromProductDtoToProduct(ProductDTO productDto) =>
            new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };
        public static ProductDTO FromProductToProductDto(Product product) =>
            new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

        public static IEnumerable<Product> FromProductsDtoToProducts(IEnumerable<ProductDTO> productsDto) =>
            productsDto.Select(productDto => new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            }).ToList();

        public static IEnumerable<ProductDTO> FromProductsToProductsDto(IEnumerable<Product> products) =>
            products.Select(product => new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            }).ToList();
    }
}
