using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriverServer.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        [JsonIgnore]
        public ICollection<GarageEmployee>? GarageEmployees { get; set; }
    }
}