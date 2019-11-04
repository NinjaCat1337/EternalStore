using EternalStore.Domain.Models;

namespace EternalStore.Domain.OrderManagement
{
    public class OrderItem : Entity
    {
        public string Name { get; set; }
        public int Qty { get; set; }

        protected OrderItem() { }

        internal static OrderItem Insert(string name, int qty) =>
            new OrderItem
            {
                Name = name,
                Qty = qty
            };
    }
}
