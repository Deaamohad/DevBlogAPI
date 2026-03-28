using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevBlogAPI.Data;
using DevBlogAPI.Models;
using DevBlogAPI.DTOs;

namespace DevBlogAPI.Controllers 
{

    [ApiController]
    [Route("api/[controller]")] 
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts(string? search, int page = 1, int pageSize = 10)
        {
            var query = _context.Posts.Include(p => p.Author).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.ToLower().Contains(search.ToLower()));
            }

            if (pageSize > 50) pageSize = 50;

            query = query.OrderByDescending(p => p.PublishedDate);

            int skipAmount = (page - 1) * pageSize;

            return await query
                .Skip(skipAmount)
                .Take(pageSize)
                .Select(p => new PostDto 
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    PublishedDate = p.PublishedDate,
                    AuthorName = p.Author.Name
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var post = await _context.Posts
            .Include(p => p.Author)
            .Select(p => new PostDto 
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                PublishedDate = p.PublishedDate,
                AuthorName = p.Author.Name
            })
            .FirstOrDefaultAsync(post => post.Id == id);

        if (post == null)
        {
            return NotFound(); 
        }

        return post;

        }



        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto createDto)
        {
            bool doesUserExist = await _context.Authors.AnyAsync(u => u.Id == createDto.AuthorId);

            if (!doesUserExist)
            {
                return BadRequest("Invalid Author ID. This user does not exist.");
            }

            var newPost = new Post
            {
                Title = createDto.Title,
                Content = createDto.Content,
                AuthorId = createDto.AuthorId,
                PublishedDate = DateTime.UtcNow 
            };

            // 3. Save to the database
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            return Ok(new PostDto 
            {
                Id = newPost.Id,
                Title = newPost.Title,
                Content = newPost.Content,
                PublishedDate = newPost.PublishedDate
            });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostDto(int id, UpdatePostDto updateDto)
        {

            Post post = await _context.Posts.FindAsync(id); 

            if (post is null)
            {
                return BadRequest();
            }

            post.Title = updateDto.Title;
            post.Content = updateDto.Content;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}