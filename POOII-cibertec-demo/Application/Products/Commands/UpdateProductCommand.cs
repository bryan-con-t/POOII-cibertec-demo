namespace POOII_cibertec_demo.Application.Products.Commands
{
    public class UpdateProductCommand
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public bool isCompleted { get; set; }
        public string ImagenPath { get; set; }
    }
}
