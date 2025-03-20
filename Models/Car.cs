using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriverServer.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string? StateNubmer { get; set; }
        public string? VIN { get; set; }

        [Required]
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        [Required]
        public int ModelId { get; set; }
        public Model? Model { get; set; }

        [Required]
        public int MarkId { get; set; }
        public Mark? Mark { get; set; }
    }
}
