namespace POOII_cibertec_demo.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string nombre { get; private set; }
        public decimal precio { get; private set; }
        public int cantidad { get; private set; }
        public bool isCompleted { get; private set; }
        public string imagenPath { get; private set; }

        public void Update(
            string nombre,
            decimal precio,
            int cantidad,
            bool isCompleted,
            string imagenPath = null)
        {
            this.nombre = nombre;
            this.precio = precio;
            this.cantidad = cantidad;
            this.isCompleted = isCompleted;

            if (!string.IsNullOrEmpty(imagenPath))
                this.imagenPath = imagenPath;
        }
    }
}
