using System.Collections.Generic;
using EternalStore.ApplicationLogic.StoreManagement.DTO;

namespace EternalStore.Api.Contracts.Order.Responses
{
    public class GetOrdersResponse
    {
        public IEnumerable<OrderDTO> Orders { get; set; }
        public int OrdersCount { get; set; }
    }
}
