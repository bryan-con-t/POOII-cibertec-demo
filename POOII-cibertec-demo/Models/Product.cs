using System.ComponentModel.DataAnnotations;

namespace POOII_cibertec_demo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string nombre { get; set; }

        public decimal precio { get; set; }

        public int cantidad { get; set; }
        
        public DateTime fechaRegistro { get; set; }

        public bool isCompleted { get; set; } = false;
    }
}
