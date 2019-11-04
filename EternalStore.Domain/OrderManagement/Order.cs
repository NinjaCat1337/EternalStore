using EternalStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.Domain.OrderManagement
{
    public class Order : Entity
    {
        public bool IsApproved { get; protected set; }
        public DateTime OrderDate { get; protected set; }
        public DateTime DeliveryDate { get; protected set; }
        public string CustomerNumber { get; protected set; }
        public string CustomerAddress { get; protected set; }
        public string AdditionalInformation { get; protected set; }
        public bool IsDelivered { get; protected set; }

        public IEnumerable<OrderItem> OrderItems => orderItems?.AsEnumerable();
        private readonly List<OrderItem> orderItems = new List<OrderItem>();

        protected Order() { }

        public static Order Insert(DateTime deliveryDate, string customerNumber, string customerAddress, string additionalInformation) =>
            new Order
            {
                IsApproved = false,
                OrderDate = DateTime.Now,
                DeliveryDate = deliveryDate,
                CustomerNumber = customerNumber,
                CustomerAddress = customerAddress,
                AdditionalInformation = additionalInformation,
                IsDelivered = false
            };

        public void ChangeAddress(string customerAddress) => CustomerAddress = customerAddress;

        public void ChangeNumber(string customerNumber) => CustomerNumber = customerNumber;

        public void ChangeDeliveryDate(DateTime deliveryDate) => DeliveryDate = deliveryDate;

        public void SetApproved() => IsApproved = true;

        public void SetDelivered() => IsDelivered = true;

        public void AddOrderItem(string name, int qty) => orderItems.Add(OrderItem.Insert(name, qty));

        public void RemoveOrderItem(int orderItemId)
        {
            var orderItem = orderItems.FirstOrDefault(oi => oi.Id == orderItemId);

            if (orderItem == null) throw new Exception("Order Item not found.");

            orderItems.Remove(orderItem);
        }
    }
}
