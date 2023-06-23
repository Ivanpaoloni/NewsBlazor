namespace NewsBlazor.Models
{
    public class News
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int authorId { get; set; }
        public int categoryId { get; set; }
        public DateTime publicationDate { get; set; }
        public string imageUrl { get; set; }
        public string content { get; set; }
        public Category category { get; set; }
        public Author author { get; set; }
    }
}
