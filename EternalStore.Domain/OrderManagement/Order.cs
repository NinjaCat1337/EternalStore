using EternalStore.Domain.Models;
using EternalStore.Domain.StoreManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalStore.Domain.OrderManagement
{
    public class Order : Entity
    {
        public bool IsApproved { get; protected set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; protected set; }
        public DateTime DeliveryDate { get; protected set; }
        public string CustomerNumber { get; protected set; }
        public string CustomerAddress { get; protected set; }
        public string AdditionalInformation { get; protected set; }
        public bool IsDelivered { get; protected set; }

        public IEnumerable<OrderItem> OrderItems => orderItems?.AsEnumerable();
        private readonly List<OrderItem> orderItems = new List<OrderItem>();

        protected Order() { }

        public static Order Insert(DateTime deliveryDate, string customerName, string customerNumber, string customerAddress, string additionalInformation)
        {
            Validate(deliveryDate, customerName, customerNumber, customerAddress, additionalInformation);

            return new Order
            {
                IsApproved = false,
                CustomerName = customerName,
                OrderDate = DateTime.Now,
                DeliveryDate = deliveryDate,
                CustomerNumber = customerNumber,
                CustomerAddress = customerAddress,
                AdditionalInformation = additionalInformation,
                IsDelivered = false
            };
        }

        public void Modify(DateTime deliveryDate, string customerName, string customerAddress, string customerNumber, string additionalInformation)
        {
            Validate(deliveryDate, customerName, customerNumber, customerAddress, additionalInformation);

            CustomerName = customerName;
            DeliveryDate = deliveryDate;
            CustomerAddress = customerAddress;
            CustomerNumber = customerNumber;
            AdditionalInformation = additionalInformation;
        }

        public void SetApproved() => IsApproved = true;

        public void SetDelivered() => IsDelivered = true;

        public void AddOrderItem(Product product, int qty) => orderItems.Add(OrderItem.Insert(this, product, qty));

        private static void Validate(DateTime deliveryDate, string customerName, string customerNumber, string customerAddress, string additionalInformation)
        {
            if (customerName.Length > 50)
                throw new Exception("Customer Name is too long.");

            if (string.IsNullOrWhiteSpace(customerName))
                throw new Exception("Customer Name can't be empty.");

            if (deliveryDate == null || deliveryDate < DateTime.Now)
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
