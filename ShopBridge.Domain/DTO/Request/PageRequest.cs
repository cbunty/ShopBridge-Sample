namespace ShopBridge.Domain.DTO.Request
{
    public class PageRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SearchParam { get; set; }

    }

}
