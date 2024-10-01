using DEPI_Final_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.Models
{
    public class CommonPersonProperties : CommonProperties
    {
        [Required]
        public int Age { get; set; }

        public string Image { get; set; } = default!;

        [Required]
        public Gender Gender { get; set; }
    }
}
