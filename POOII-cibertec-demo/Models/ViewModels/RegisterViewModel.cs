using System.ComponentModel.DataAnnotations;

namespace POOII_cibertec_demo.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; }
    }
}
