using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.Models
{
    public class EmployeeProjects
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = default!;

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; } = default!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Hours { get; set; }
    }
}
