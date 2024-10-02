using DEPI_Final_Project.Attributes;
using DEPI_Final_Project.Settings;

namespace DEPI_Final_Project.ViewModels.EmployeeVM
{
    public class CreateEmployeeVM : CommonEmployeeVM
    {
        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Image { get; set; } = default!;
    }
}
