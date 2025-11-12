using Microsoft.AspNetCore.Identity;

namespace POOII_cibertec_demo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
