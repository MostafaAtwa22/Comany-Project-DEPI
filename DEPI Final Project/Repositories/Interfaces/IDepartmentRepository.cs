using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
