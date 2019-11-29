using EternalStore.Domain.Models;
using EternalStore.Domain.StoreManagement;
using System;

namespace EternalStore.Domain.OrderManagement
{
    public class OrderItem : Entity
    {
        public virtual Product Product { get; protected set; }
        public int Qty { get; protected set; }

        public virtual Order Order { get; protected set; }


        protected OrderItem() { }

        internal static OrderItem Insert(Order order, Product product, int qty)
        {
            if (qty <= 0)
                throw new Exception("Quantity cannot be equal or less equal zero.");

            return new OrderItem
            {
                Order = order,
                Product = product,
                Qty = qty
            };
        }
    }
}
