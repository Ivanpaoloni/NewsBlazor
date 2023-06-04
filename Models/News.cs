namespace NewsBlazor.Models
{
    public class News
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public DateTime publicationDate { get; set; }
        public string imageUrl { get; set; }
        public string content { get; set; }
    }
}
