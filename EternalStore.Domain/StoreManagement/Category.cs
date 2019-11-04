using EternalStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.Domain.StoreManagement
{
    public class Category : Entity
    {
        public string Name { get; protected set; }
        public bool IsEnabled { get; protected set; }

        public IEnumerable<Product> Products => products?.AsEnumerable();
        private readonly List<Product> products = new List<Product>();

        protected Category() { }

        public static Category Insert(string name)
        {
            Validate(name);

            return new Category { Name = name, IsEnabled = true };
        }

        public void Modify(string name)
        {
            Validate(name);

            Name = name;
        }

        public void Enable() => IsEnabled = true;

        public void Disable() => IsEnabled = false;

        public void AddProduct(string name, string description, decimal price) =>
            products.Add(Product.Insert(name, description, price, this));

        public void EditProduct(int productId, string name, string description, decimal price)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
                throw new Exception("Product not found.");

            product.Modify(name, description, price);
        }

        public void RemoveProduct(int productId)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
                throw new Exception("Product not found.");

            products.Remove(product);
        }

        private static void Validate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty.");

            if (name.Length > 50)
                throw new Exception("Name is too long.");
        }
    }
}
