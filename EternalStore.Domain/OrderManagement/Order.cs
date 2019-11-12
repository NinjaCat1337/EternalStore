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

        public void Modify(DateTime deliveryDate, string customerAddress, string customerNumber, string additionalInformation)
        {
            Validate(deliveryDate, customerNumber, customerAddress, additionalInformation);

            DeliveryDate = deliveryDate;
            CustomerAddress = customerAddress;
            CustomerNumber = customerNumber;
            AdditionalInformation = additionalInformation;
        }

        public void SetApproved() => IsApproved = true;

        public void SetDelivered() => IsDelivered = true;

        public void AddOrderItem(string name, int qty) => orderItems.Add(OrderItem.Insert(this, name, qty));

        private static void Validate(DateTime deliveryDate, string customerNumber, string customerAddress, string additionalInformation)
        {
            if (deliveryDate == null || deliveryDate > DateTime.Now)
                throw new Exception("Wrong delivery date.");

            if (string.IsNullOrWhiteSpace(customerNumber) || customerNumber.Length > 30)
                throw new Exception("Wrong number.");

            if (customerAddress.Length > 100)
                throw new Exception("Address is too long.");

            if (string.IsNullOrWhiteSpace(customerAddress))
                throw new Exception("Address can't be empty.");

            if (additionalInformation.Length > 100)
                throw new Exception("Additional information is too long.");
        }
    }
}
