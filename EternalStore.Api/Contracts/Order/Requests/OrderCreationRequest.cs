using System;
using System.Collections.Generic;
using EternalStore.ApplicationLogic.StoreManagement.DTO;

namespace EternalStore.Api.Contracts.Order.Requests
{
    public class OrderCreationRequest
    {
        public DateTime DeliveryDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
