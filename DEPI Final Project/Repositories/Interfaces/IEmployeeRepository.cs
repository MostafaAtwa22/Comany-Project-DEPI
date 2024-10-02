using DEPI_Final_Project.Models;
using DEPI_Final_Project.ViewModels.EmployeeVM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<SelectListItem> GetSelectList();
        IEnumerable<SelectListItem> GetUnassignedManagers();
        IEnumerable<SelectListItem> GetManagersForEdit(int? currentManagerId);
        IEnumerable<Employee> GetAll();
        Employee? GetById(int id);
        Task Create(CreateEmployeeVM model);
        Task<Employee?> Update(EditEmployeeVM model);
        bool Delete(int id);
    }
}
