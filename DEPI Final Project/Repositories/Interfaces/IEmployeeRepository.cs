using DEPI_Final_Project.Models;
using DEPI_Final_Project.ViewModels;

namespace DEPI_Final_Project.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        Task Create(CreateEmployeeVM model);
        Task<Employee?> Update(EditEmployeeVM model);
        bool Delete(int id);
    }
}
