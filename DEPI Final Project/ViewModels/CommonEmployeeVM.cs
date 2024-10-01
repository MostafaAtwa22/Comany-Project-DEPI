using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.ViewModels
{
    public class CommonEmployeeVM
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Address { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int Gender { get; set; }

        public IEnumerable<SelectListItem> GenderSelectList { get; set; } = Enumerable.Empty<SelectListItem>();

            [Required]
            [Display(Name = "Department")]
            public int DepartmentId { get; set; }

            public IEnumerable<SelectListItem> Departments { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Projects")]
        public List<int> SelectedProjects { get; set; } = default!;

        public IEnumerable<SelectListItem> Projects = Enumerable.Empty<SelectListItem>();
    }
}
