using EternalStore.Domain.Models;

namespace EternalStore.Domain.OrderManagement
{
    public class OrderItem : Entity
    {
        public string Name { get; protected set; }
        public int Qty { get; protected set; }

        public virtual Order Order { get; protected set; }


        protected OrderItem() { }

        internal static OrderItem Insert(Order order, string name, int qty) =>
            new OrderItem
            {
                Order = order,
                Name = name,
                Qty = qty
            };
    }
}
