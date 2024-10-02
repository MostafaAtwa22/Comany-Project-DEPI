using DEPI_Final_Project.Data;
using DEPI_Final_Project.Models;
using DEPI_Final_Project.Models.Enums;
using DEPI_Final_Project.Repositories.Interfaces;
using DEPI_Final_Project.Settings;
using DEPI_Final_Project.ViewModels.DependentVM;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Final_Project.Repositories
{
    public class DependentRepository : IDependentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public DependentRepository(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public IEnumerable<Dependent> GetAll()
            => _context.Dependents
            .Include(d => d.Employee)
            .AsNoTracking()
            .ToList();

        Dependent? IDependentRepository.GetById(int empId, string name)
            => _context.Dependents
            .Include(d => d.Employee)
            .AsNoTracking()
            .SingleOrDefault(d => d.EmployeeId == empId && d.Name == name);

        public IEnumerable<Dependent?> GetByEmployee(int id)
            => _context.Dependents
            .Include(d => d.Employee)
            .AsNoTracking()
            .Where(d => d.Employee.Id == id)
            .ToList();

        public async Task Create(CreateDependentVM model)
        {
            var imageName = await SaveImage(model.Image);

            Dependent dependent = new Dependent
            {
                Name = model.Name,
                EmployeeId = model.EmployeeId,
                Gender = (Gender)model.Gender,
                Image = imageName
            };

            _context.Dependents.Add(dependent);
            _context.SaveChanges();
        }

        public bool Delete(int id, string name)
        {
            var isDeleted = false;
            var dependent = _context.Dependents
                .SingleOrDefault(d => d.EmployeeId == id && d.Name == name);

            if (dependent is null)
                return false;

            _context.Dependents.Remove(dependent);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;

                var image = Path.Combine(_imagesPath, dependent.Image);
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
