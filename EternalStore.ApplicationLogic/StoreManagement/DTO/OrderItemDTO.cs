namespace EternalStore.ApplicationLogic.StoreManagement.DTO
{
    public class OrderItemDTO
    {
        public int IdOrderItem { get; set; }
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public int IdCategory { get; set; }
        public int Qty { get; set; }
    }
}
