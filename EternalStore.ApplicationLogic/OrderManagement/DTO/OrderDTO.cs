using System;
using System.Collections.Generic;

namespace EternalStore.ApplicationLogic.OrderManagement.DTO
{
    public class OrderDTO
    {
        public int IdOrder { get; set; }
        public bool IsApproved { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerAddress { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public string AdditionalInformation { get; set; }
        public bool IsDelivered { get; set; }
    }
}
