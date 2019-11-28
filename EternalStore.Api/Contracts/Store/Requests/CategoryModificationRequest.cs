namespace EternalStore.Api.Contracts.Store.Requests
{
    public class CategoryModificationRequest
    {
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
