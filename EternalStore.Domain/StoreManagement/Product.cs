using EternalStore.Domain.Models;
using EternalStore.Domain.OrderManagement;
using System;
using System.Collections.Generic;

namespace EternalStore.Domain.StoreManagement
{
    public class Product : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public decimal Price { get; protected set; }
        public virtual Category Category { get; protected set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        protected Product() { }

        internal static Product Insert(Category category, string name, string description, decimal price)
        {
            Validate(name, description, price);

            return new Product
            {
                Name = name,
                Description = description,
                Price = price,
                Category = category
            };
        }

        internal void Modify(string name, string description, decimal price)
        {
            Validate(name, description, price);

            Name = name;
            Description = description;
            Price = price;
        }

        private static void Validate(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
                throw new Exception("Name or Description can't be empty.");

            if (name.Length > 100)
                throw new Exception("Name is too long.");

            if (description.Length > 1500)
                throw new Exception("Description is too long.");

            if (price <= 0)
                throw new Exception("Price can't be less or equal zero.");
        }
    }
}
