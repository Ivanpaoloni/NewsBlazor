namespace NewsBlazor.Models
{
    public class NewsPaginatedList
    {
        public List<News>? items { get; set; }
        public int totalItems { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
