using System;

namespace EternalStore.Api.Contracts.Order.Requests
{
    public class GetOrdersRequest
    {
        public int? Count { get; set; }
        public int? Page { get; set; }

        public bool? Ascending { get; set; }
        public DateTime? OrderDateFrom { get; set; }
        public DateTime? OrderDateTo { get; set; }
        public DateTime? DeliveryDateFrom { get; set; }
        public DateTime? DeliveryDateTo { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsDelivered { get; set; }
    }
}
