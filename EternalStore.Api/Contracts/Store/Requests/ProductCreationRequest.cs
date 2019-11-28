namespace EternalStore.Api.Contracts.Store.Requests
{
    public class ProductCreationRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int IdCategory { get; set; }
    }
}
