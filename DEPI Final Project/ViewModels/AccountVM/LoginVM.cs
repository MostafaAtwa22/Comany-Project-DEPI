using System.ComponentModel.DataAnnotations;

namespace DEPI_Final_Project.ViewModels.AccountVM
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RemeberMe { get; set; }
    }
}
