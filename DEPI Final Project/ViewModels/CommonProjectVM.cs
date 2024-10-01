using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.ViewModels
{
    public class CommonProjectVM
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(1_00_000, 10_000_000)]
        public int Budget { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
