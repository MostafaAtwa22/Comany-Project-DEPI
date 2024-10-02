using DEPI_Final_Project.Attributes;
using DEPI_Final_Project.Settings;

namespace DEPI_Final_Project.ViewModels.DependentVM
{
    public class EditDependentVM : CommonDependentVM
    {
        public string? CurrentImage { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Image { get; set; } = default!;
    }
}
