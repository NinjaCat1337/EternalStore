namespace EternalStore.Api.Contracts.Order.Requests
{
    public class OrderItemAdditionRequest
    {
        public int IdOrder { get; set; }
        public int IdCategory { get; set; }
        public int IdProduct { get; set; }
        public int Qty { get; set; }
    }
}
