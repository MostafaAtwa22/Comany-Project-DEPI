using DEPI_Final_Project.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.Models
{
    public class Project : CommonProperties, ISoftDeleteable
    {
        [Required]
        [Range(1_00_000, 10_000_000)]
        public int Budget { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;

        public virtual ICollection<Employee>? Employees { get; set; } = new List<Employee>();

        public virtual ICollection<EmployeeProjects>? EmployeeProjects { get; set; } = new List<EmployeeProjects>();

        public int DepartmentId { get; set; }

        public Department Department { get; set; } = default!;

        public bool IsDeleted { get; set; }

        public DateTime? DateOFDelete { get; set; }
    }
}
