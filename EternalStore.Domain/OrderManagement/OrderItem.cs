using EternalStore.Domain.Models;

namespace EternalStore.Domain.OrderManagement
{
    public class OrderItem : Entity
    {
        public string Name { get; protected set; }
        public int Qty { get; protected set; }

        public int OrderId { get; protected set; }
        public virtual Order Order { get; protected set; }


        protected OrderItem() { }

        internal static OrderItem Insert(string name, int qty) =>
            new OrderItem
            {
                Name = name,
                Qty = qty
            };
    }
}
