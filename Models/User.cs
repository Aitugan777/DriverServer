using System.ComponentModel.DataAnnotations;

namespace DriverServer.Models
{
    // Модель для пользователя
    public class User
    {
        [Key]
        public int Id { get; set; } // Идентификатор пользователя, теперь int

        [StringLength(100)]
        public string? FirstName { get; set; } // Имя

        [StringLength(100)]
        public string? LastName { get; set; } // Фамилия

        [StringLength(100)]
        public string? MiddleName { get; set; } // Отчество

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;// Электронная почта

        [Required]
        [StringLength(200)]
        public string PasswordHash { get; set; } = null!;// Хэш пароля

        [Required]
        public bool IsActive { get; set; } // Статус активности пользователя

        public DateTime CreatedDate { get; set; } // Дата регистрации пользователя

        [StringLength(100)]
        public string? GoogleSub { get; set; } // Идентификатор пользователя из Google (если есть)

        [StringLength(20)]
        public string? PhoneNumber { get; set; } // Телефонный номер пользователя
    }
}
