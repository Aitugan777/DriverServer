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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("Пользовательские данные не могут быть пустыми.");
            }

            // Находим пользователя по Id
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Пользователь не найден.");
            }

            // Обновляем данные пользователя
            user.FirstName = updatedUser.FirstName ?? user.FirstName;
            user.LastName = updatedUser.LastName ?? user.LastName;
            user.MiddleName = updatedUser.MiddleName ?? user.MiddleName;
            user.Email = updatedUser.Email ?? user.Email;
            user.PhoneNumber = updatedUser.PhoneNumber ?? user.PhoneNumber;
            user.IsActive = updatedUser.IsActive;
            user.GoogleSub = updatedUser.GoogleSub ?? user.GoogleSub;

            // Сохраняем изменения в базе данных
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}
