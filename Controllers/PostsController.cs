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
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            return await _context.Posts
            .Include(p => p.Author)
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
        public async Task<ActionResult<PostDto>> CreatePost(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id) 
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Post post)
        {
            if (post.Id != id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Posts.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw; 
                }
            }
            return NoContent();
        }
    }
}