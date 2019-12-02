namespace EternalStore.ApplicationLogic.OrderManagement.DTO
{
    public class OrderItemDTO
    {
        public int IdOrderItem { get; set; }
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        public int Qty { get; set; }
    }
}
