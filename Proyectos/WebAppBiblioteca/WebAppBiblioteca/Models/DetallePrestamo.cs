namespace WebAppBiblioteca.Models
{
    public class DetallePrestamo
    {
        public int Id { get; set; }

        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; } = new();

        public int LibroId { get; set; }
        public Libro Libro { get; set; } = new();
    }
}
