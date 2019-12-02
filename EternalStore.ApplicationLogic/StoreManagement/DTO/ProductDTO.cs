namespace EternalStore.ApplicationLogic.StoreManagement.DTO
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
    }
}
