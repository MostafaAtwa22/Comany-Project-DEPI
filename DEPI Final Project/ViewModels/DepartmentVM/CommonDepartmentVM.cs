using DEPI_Final_Project.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.ViewModels.DepartmentVM
{
    public class CommonDepartmentVM
    {
        [Required]
        [MaxLength(50)]
        [UniqueDepartmentName]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = null!;

        [Required]
        [Display(Name = "Manager Name")]
        public int? ManagerId { get; set; }

        public IEnumerable<SelectListItem> ManagersSelectList = Enumerable.Empty<SelectListItem>();
    }
}
