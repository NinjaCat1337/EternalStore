using EternalStore.ApplicationLogic.OrderManagement.DTO;
using System.Collections.Generic;

namespace EternalStore.Api.Contracts.Order.Responses
{
    public class GetOrdersResponse
    {
        public IEnumerable<OrderDTO> Orders { get; set; }
        public int OrdersCount { get; set; }
    }
}
