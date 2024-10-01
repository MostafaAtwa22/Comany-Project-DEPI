using DEPI_Final_Project.Data;
using DEPI_Final_Project.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
            => _context.Departments
            .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
            .OrderBy(d => d.Text)
            .AsNoTracking()
            .ToList();
    }
}
