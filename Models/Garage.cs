using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriverServer.Models
{
    public class Garage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }
        [JsonIgnore]
        public User? Owner { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        public int PositionId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        [JsonIgnore]
        public ICollection<GarageEmployee>? GarageEmployees { get; set; }
    }
}