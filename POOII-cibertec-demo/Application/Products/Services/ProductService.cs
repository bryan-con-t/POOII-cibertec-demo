using POOII_cibertec_demo.Application.Products.Commands;
using POOII_cibertec_demo.Infrastructure.Persistence;

namespace POOII_cibertec_demo.Application.Products.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repo;

        public ProductService(ProductRepository repo)
        {
            _repo = repo;
        }

        public async Task UpdateAsync(UpdateProductCommand command)
        {
            var product = await _repo.GetByIdAsync(command.Id);
            if (product == null)
                throw new Exception("Producto no encontrado");

            product.Update(
                command.nombre,
                command.precio,
                command.cantidad,
                command.isCompleted,
                command.ImagenPath
            );

            await _repo.SaveAsync();
        }
    }
}
