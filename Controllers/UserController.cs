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

        static Dictionary<string, int> _confirmCodes = new Dictionary<string, int>();

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Проверка на не использование почты
        /// </summary>
        /// <param name="email">почта</param>
        /// <returns>ok, если почта не используется, BadRequest если почта используется</returns>
        [HttpGet("IsEmailFree/{email}")]
        public async Task<IActionResult> IsEmailFree(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (!await _dbContext.Users.AnyAsync(x => x.Email == email))
                    return Ok();
            }
            return BadRequest("Такой Email уже используется");
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
    }
}
