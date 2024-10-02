using DEPI_Final_Project.Models;
using DEPI_Final_Project.ViewModels.ProjectVM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<SelectListItem> GetSelectList();
        IEnumerable<Project> GetAll();
        Project? GetById(int id);
        Task Create(CreateProjectVM model);
        Task<Project?> Update(EditProjectVM model);
        bool Delete(int id);
    }
}
