using DEPI_Final_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.ViewModels.DependentVM
{
    public class CommonDependentVM
    {
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Gender")]
        public int Gender { get; set; }

        public IEnumerable<SelectListItem> GenderSelectList { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required]
        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }

        public IEnumerable<SelectListItem> EmployeesList = Enumerable.Empty<SelectListItem>();
    }
}
