using Microsoft.EntityFrameworkCore;
using DevBlogAPI.Models; 

namespace DevBlogAPI.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}