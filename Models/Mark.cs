using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriverServer.Models
{
    public class Mark
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int ModelId { get; set; }
        public Model? Model { get; set; }

    }
}
