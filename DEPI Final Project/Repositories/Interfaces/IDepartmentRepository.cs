using DEPI_Final_Project.Models;
using DEPI_Final_Project.ViewModels;
using DEPI_Final_Project.ViewModels.DepartmentVM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<SelectListItem> GetSelectList();
        IEnumerable<Department> GetAll();
        Department? GetById(int id);
        Task Create(CreateDepartmentVM model);
        Task<Department?> Update(EditDepartmentVM model);
        bool Delete(int id);
    }
}
