using DriverServer.Data;
using DriverServer.Interfaces;
using DriverServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DriverServer.Controllers
{
    /// <summary>
    /// Контроллер для регистрации и подтверждения email.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private static readonly Dictionary<string, int> _confirmationCodes = new();

        /// <summary>
        /// Конструктор контроллера с внедрением зависимостей.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="emailService">Сервис отправки email.</param>
        public RegistrController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        /// <summary>
        /// Регистрация нового пользователя. Если Email не найден, отправляет код подтверждения на почту.
        /// </summary>
        /// <param name="user">Объект пользователя.</param>
        /// <returns>Ответ с подтверждением отправки кода.</returns>
        [HttpGet("register/{email}")]
        public IActionResult Register(string email)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                return BadRequest("Пользователь с таким Email уже существует.");
            }

            var confirmationCode = new Random().Next(1000, 9999);

            if (_confirmationCodes.ContainsKey(email))
                _confirmationCodes[email] = confirmationCode;
            else
                _confirmationCodes.Add(email, confirmationCode);

            _emailService.SendConfirmLetter(email, confirmationCode.ToString());

            return Ok("Код подтверждения отправлен на Email.");
        }

        /// <summary>
        /// Подтверждение email с использованием кода. Если код верный, создается новый пользователь.
        /// </summary>
        /// <param name="request">Запрос с Email, кодом и паролем.</param>
        /// <returns>Ответ с результатом подтверждения.</returns>
        [HttpPost("confirm")]
        public IActionResult ConfirmEmail([FromBody] ConfirmRequest request)
        {
            User user = request.User;
            if (user != null)
            {
                if (_confirmationCodes.TryGetValue(user.Email, out int storedCode) && storedCode == request.Code)
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    _confirmationCodes.Remove(user.Email);

                    return Ok("Регистрация успешно завершена.");
                }
                else
                    return BadRequest("Неверный код подтверждения.");
            }
            else
                return BadRequest("User is null.");
        }
    }
}
