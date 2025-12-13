using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POOII_cibertec_demo.Models;
using POOII_cibertec_demo.Domain.Entities;

namespace POOII_cibertec_demo.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<POOII_cibertec_demo.Domain.Entities.Product> Products { get; set; }
    }
}
