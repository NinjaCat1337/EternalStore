using System;

namespace EternalStore.Api.Contracts.Order.Requests
{
    public class OrderModificationRequest
    {
        public DateTime DeliveryDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerNumber { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
