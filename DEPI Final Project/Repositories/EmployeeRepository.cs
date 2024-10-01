using DEPI_Final_Project.Data;
using DEPI_Final_Project.Models.Enums;
using DEPI_Final_Project.Models;
using DEPI_Final_Project.Repositories.Interfaces;
using DEPI_Final_Project.Settings;
using DEPI_Final_Project.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Final_Project.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public EmployeeRepository(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public IEnumerable<Employee> GetAll()
            => _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Projects)
            .AsNoTracking()
            .ToList();

        public Employee? GetById(int id)
            => _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Projects)
            .AsNoTracking()
            .SingleOrDefault(e => e.Id == id);

        public async Task Create(CreateEmployeeVM model)
        {
            var imageName = await SaveImage(model.Image);

            Employee employee = new Employee
            {
                Name = model.Name,
                DepartmentId = model.DepartmentId,
                Gender = (Gender)model.Gender,
                Age = model.Age,
                Salary = model.Salary,
                EmployeeProjects = model.SelectedProjects
                   .Select(projectId => new EmployeeProjects
                   {
                       ProjectId = projectId,
                       StartDate = DateTime.Now, 
                       Hours = 0 
                   }).ToList(),
                Image = imageName
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
        }


        public async Task<Employee?> Update(EditEmployeeVM model)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .SingleOrDefault(e => e.Id == model.Id);

            if (employee is null)
                return null!;

            var hasNewImage = model.Image is not null;
            var oldImage = employee.Image;

            employee.Gender = (Gender)model.Gender;
            employee.Name = model.Name;
            employee.DepartmentId = model.DepartmentId;
            employee.Age = model.Age;
            employee.Salary = model.Salary;
            employee.Address = model.Address;
            employee.EmployeeProjects = model.SelectedProjects
                 .Select(projectId => new EmployeeProjects
                 {
                     ProjectId = projectId,
                     StartDate = DateTime.Now,
                     Hours = 0
                 }).ToList();

            if (hasNewImage)
                employee.Image = await SaveImage(model.Image!);

            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var image = await SaveImage(model.Image!);
                    File.Delete(image);
                }

                return employee;
            }
            else
            {
                var image = Path.Combine(_imagesPath, employee.Image);
                File.Delete(image);

                return null!;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var employee = GetById(id);

            if (employee is null)
                return false;

            _context.Employees.Remove(employee);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;

                var image = Path.Combine(_imagesPath, employee.Image);
                if (File.Exists(image))
                {
                    File.Delete(image);
                }
            }
            return isDeleted;
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            var path = Path.Combine(_imagesPath, imageName);

            using var stream = File.Create(path);
            await image.CopyToAsync(stream);

            return imageName;
        }
    }
}
