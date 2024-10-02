using DEPI_Final_Project.Models;
using DEPI_Final_Project.ViewModels;
using DEPI_Final_Project.ViewModels.DependentVM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Repositories.Interfaces
{
    public interface IDependentRepository
    {
        IEnumerable<Dependent> GetAll();
        Dependent? GetById(int empId, string name);
        IEnumerable<Dependent?> GetByEmployee(int id);
        Task Create(CreateDependentVM model);
        bool Delete(int id, string name);
    }
}
