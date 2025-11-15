using System.ComponentModel.DataAnnotations;

namespace POOII_cibertec_demo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string nombre { get; set; }

        public decimal precio { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
