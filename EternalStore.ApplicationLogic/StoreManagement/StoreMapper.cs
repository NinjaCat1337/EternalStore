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
                Id = category.Id,
                Name = category.Name
            }).ToList();

        public static IEnumerable<ProductDTO> FromProductsToProductsDTO(IEnumerable<Product> products) =>
            products.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IdCategory = product.Category.Id
            }).ToList();

        public static ProductDTO FromProductToProductDTO(Product product) =>
            new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IdCategory = product.Category.Id
            };

        public static CategoryDTO FromCategoryToCategoryDTO(Category category) =>
            new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
    }
}
