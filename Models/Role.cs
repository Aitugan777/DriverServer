using System.ComponentModel.DataAnnotations;

namespace DriverServer.Models
{
    // Модель для роли
    public class Role
    {
        [Key]
        public int Id { get; set; } // Идентификатор роли, теперь int

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!; // Название роли (например, Администратор, Механик)
    }
}
