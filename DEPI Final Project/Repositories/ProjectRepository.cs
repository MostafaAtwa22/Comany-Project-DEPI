using DEPI_Final_Project.Data;
using DEPI_Final_Project.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DEPI_Final_Project.Models;
using DEPI_Final_Project.ViewModels.ProjectVM;

namespace DEPI_Final_Project.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
            => _context.Projects
            .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
            .OrderBy(d => d.Text)
            .AsNoTracking()
            .ToList();

        public IEnumerable<Project> GetAll()
            => _context.Projects
            .Include(e => e.Department)
            .AsNoTracking()
            .ToList();

        public Project? GetById(int id)
            => _context.Projects
            .Include(e => e.Department)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);

        public async Task Create(CreateProjectVM model)
        {
            Project project = new Project
            {
                Name = model.Name,
                DepartmentId = model.DepartmentId,
                Location = model.Location,
                Budget = model.Budget
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var project = _context.Projects
                .SingleOrDefault(p => p.Id == id);

            if (project is null)
                return false;

            _context.Projects.Remove(project);
            var affectedRows = _context.SaveChanges();

            return affectedRows > 0;
        }

        public async Task<Project?> Update(EditProjectVM model)
        {
            var project = _context.Projects
                .Include(p => p.Department)
                .SingleOrDefault(p => p.Id ==  model.Id);

            if (project is null)
                return null!;

            project.Name = model.Name;
            project.Location = model.Location;
            project.DepartmentId = model.DepartmentId;
            project.Budget = model.Budget;

            await _context.SaveChangesAsync();
            return project;
        }
    }
}
