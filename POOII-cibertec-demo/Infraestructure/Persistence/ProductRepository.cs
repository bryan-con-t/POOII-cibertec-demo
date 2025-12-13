using Microsoft.EntityFrameworkCore;
using POOII_cibertec_demo.Data;
using POOII_cibertec_demo.Domain.Entities;

namespace POOII_cibertec_demo.Infrastructure.Persistence
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
