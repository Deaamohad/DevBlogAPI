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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Deaa Mohammed", Email = "deaa@deaa.com" }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post 
                { 
                    Id = 1, 
                    Title = "My First C# Post", 
                    Content = "Hello!!!", 
                    PublishedDate = new DateTime(2026, 3, 26),
                    AuthorId = 1 
                },
                new Post 
                { 
                    Id = 2, 
                    Title = "HI I'm a post!", 
                    Content = "I'm inside a container.", 
                    PublishedDate = new DateTime(2026, 3, 26),
                    AuthorId = 1 
                }
            );
        }
    }
}