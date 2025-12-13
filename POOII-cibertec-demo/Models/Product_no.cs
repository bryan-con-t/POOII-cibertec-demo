using System.ComponentModel.DataAnnotations;

namespace POOII_cibertec_demo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        public string nombre { get; set; }

        [Range(0.1, 10000, ErrorMessage = "El precio debe estar entre 0 y 10000.")]
        public decimal precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Cantidad inválida")]
        public int cantidad { get; set; }

        [DataType(DataType.Date)]
        public DateTime fechaRegistro { get; set; } = DateTime.UtcNow;

        public bool isCompleted { get; set; } = false;

        // Nueva propiedad para la imagen
        [StringLength(250)]
        [Display(Name = "Imagen del producto")]
        public string? imagenPath { get; set; }

    }
}

