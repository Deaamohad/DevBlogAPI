using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevBlogAPI.Data;
using DevBlogAPI.Models;
using DevBlogAPI.DTOs;

namespace DevBlogAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]

    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = _context.Authors;

            return await authors
                .Select(a => new AuthorDto 
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email,
                })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor(CreateAuthorDto authorDto)
        {

            var newAuthor = new Author
            {
                Name = authorDto.Name,
                Email = authorDto.Email
            };

            _context.Authors.Add(newAuthor);

            await _context.SaveChangesAsync();

            return Ok(new AuthorDto
            {
                Id = newAuthor.Id,
                Name = newAuthor.Name,
                Email = newAuthor.Email
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, CreateAuthorDto updateAuthor)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author is null)
            {
                return NotFound();
            }

            author.Name = updateAuthor.Name;
            author.Email = updateAuthor.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author is null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}