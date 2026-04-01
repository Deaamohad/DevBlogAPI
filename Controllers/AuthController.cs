using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevBlogAPI.DTOs;
using DevBlogAPI.Data;

namespace DevBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Email == request.Email);

            if (author == null || request.Password != "password") 
            {
                return Unauthorized("Invalid email or password.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "DevBlog",
                audience: "DevBlog",
                claims: new[] { 
                    new Claim(ClaimTypes.Email, author.Email),
                    new Claim(ClaimTypes.Name, author.Name), // Now using real name from DB
                    new Claim("AuthorId", author.Id.ToString()) 
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}