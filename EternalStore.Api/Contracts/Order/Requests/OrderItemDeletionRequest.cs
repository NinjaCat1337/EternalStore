namespace EternalStore.Api.Contracts.Order.Requests
{
    public class OrderItemDeletionRequest
    {
        public int IdOrder { get; set; }
        public int IdOrderItem { get; set; }
    }
}
