using Microsoft.EntityFrameworkCore;
using POOII_cibertec_demo.Data;
using POOII_cibertec_demo.Models;
using POOII_cibertec_demo.Models.ViewModels;

namespace POOII_cibertec_demo.Repositories
{
    public class ProductCommand
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public bool isCompleted { get; set; }

        // Método para actualizar producto en EF
        public void UpdateProduct(Product product)
        {
            product.nombre = nombre;
            product.precio = precio;
            product.cantidad = cantidad;
            product.isCompleted = isCompleted;
        }
    }
}
