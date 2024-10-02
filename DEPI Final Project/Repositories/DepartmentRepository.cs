using DEPI_Final_Project.Data;
using DEPI_Final_Project.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DEPI_Final_Project.Models;


namespace DEPI_Final_Project.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
            => _context.Departments
            .Include(d => d.Manager)
            .AsNoTracking()
            .ToList();

        public Department? GetById(int id)
            => _context.Departments
            .Include(d => d.Manager)
            .AsNoTracking()
            .SingleOrDefault(d => d.Id == id);

        public IEnumerable<SelectListItem> GetSelectList()
            => _context.Departments
            .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
            .OrderBy(d => d.Text)
            .AsNoTracking()
            .ToList();

        public async Task Create(CreateDepartmentVM model)
        {
            var Department = new Department()
            {
                Location = model.Location,
                ManagerId = model.ManagerId,
                Name = model.Name,
            };

            _context.Add(Department);
            await _context.SaveChangesAsync();

            // change the Manager Department
            var manager = _context.Employees.Find(model.ManagerId);
            var department = _context.Departments.SingleOrDefault(d => d.Name == Department.Name);
            manager!.DepartmentId = department!.Id;

            await _context.SaveChangesAsync();
        }

        public async Task<Department?> Update(EditDepartmentVM model)
        {
            var department = _context.Departments
                .Include(d => d.Manager)
                .SingleOrDefault(d => d.Id == model.Id);

            if (department is null)
                return null!;

            department.Name = model.Name;
            department.Location = model.Location;
            department.ManagerId = model.ManagerId;

            // change the Manager Department
            var manager = _context.Employees.Find(model.ManagerId);
            manager!.DepartmentId = model.Id;

            await _context.SaveChangesAsync();
            return department;
        }

        public bool Delete(int id)
        {
            var department = _context.Departments
                .SingleOrDefault(d => d.Id == id);

            if (department is null)
                return false;

            _context.Remove(department);

            return _context.SaveChanges() > 0;
        }
    }
}
