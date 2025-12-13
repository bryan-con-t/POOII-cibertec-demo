using System.ComponentModel.DataAnnotations;

namespace POOII_cibertec_demo.Models.ViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string nombre { get; set; }

        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public bool isCompleted { get; set; }
        public string? imagenPath { get; set; }
    }
}
