using DriverServer.Data;
using DriverServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriverServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        ApplicationDbContext _dbContext = null!;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Добавление пользователя
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("Email уже зарегистрирован");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.CreatedDate = DateTime.UtcNow;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok(user);
        }

        // Удаление пользователя
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok("Пользователь удален");
        }

        // Получение пользователя по Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }
            return Ok(user);
        }

        // Получение пользователя по Email и Password
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Неверный email или пароль");
            }
            return Ok(user);
        }
    }
}
