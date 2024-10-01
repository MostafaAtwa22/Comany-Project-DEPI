using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DEPI_Final_Project.Models
{
    public class CommonProperties
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
