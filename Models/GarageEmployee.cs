using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriverServer.Models
{
    public class GarageEmployee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GarageId { get; set; }
        [JsonIgnore]
        public Garage? Garage { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}