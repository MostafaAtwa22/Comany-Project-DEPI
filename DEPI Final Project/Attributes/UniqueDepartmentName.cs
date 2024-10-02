using DEPI_Final_Project.Data;
using DEPI_Final_Project.ViewModels.DepartmentVM;
using System.ComponentModel.DataAnnotations;

public class UniqueDepartmentName : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success!;

        var _context = validationContext.GetService<ApplicationDbContext>();

        if (_context is null)
            throw new InvalidOperationException("Database context not available.");

        string? name = value.ToString()?.ToLower();

        bool nameExists = IsDepartmentNameTaken(_context, name, validationContext.ObjectInstance);

        if (nameExists)
            return new ValidationResult("The Department Name must be unique.");

        return ValidationResult.Success!;
    }

    private bool IsDepartmentNameTaken(ApplicationDbContext context, string? name, object viewModel)
    {
        if (viewModel is CreateDepartmentVM)
            return context.Departments.Any(d => d.Name.ToLower() == name);

        else if (viewModel is EditDepartmentVM editDept)
            return context.Departments.Any(d => d.Name.ToLower() == name && d.Id != editDept.Id);

        return false;
    }
}
