using System.ComponentModel.DataAnnotations;

namespace DriverServer.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

    }
}
