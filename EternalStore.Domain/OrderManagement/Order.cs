using System;

namespace EternalStore.Domain.OrderManagement
{
    public class Order
    {
        public int OrderId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string OrderItems { get; set; }
        public string AdditionalInformation { get; set; }
        public bool IsDelivered { get; set; }
    }
}
