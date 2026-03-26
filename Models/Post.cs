namespace DevBlogAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        public int AuthorId { get; set; }
        
        public Author? Author { get; set; }
    }
}