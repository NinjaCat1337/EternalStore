namespace EternalStore.Api.Contracts.Goods.Requests
{
    public class CategoryModificationRequest
    {
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
