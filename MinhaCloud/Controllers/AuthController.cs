using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinhaCloud.Data;
using MinhaCloud.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinhaCloud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly string _jwtKey = "minhacloud-chave-secreta-super-segura-2024-xyz!@#";

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            if (_db.Users.Any(u => u.Email == req.Email))
                return BadRequest("E-Mail ja cadastrado");

            var user = new User
            {
                UserName = req.Username,
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password)

            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok("Usuario criado com sucesso!");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == req.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized("E-Mail ou senha Invalidos!");

            var token = GerarToken(user);
            return Ok(new { token });
        }

        private string GerarToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

    public record RegisterRequest(string Username, string Email, string Password);
    public record LoginRequest(string Email, string Password);
            
}
