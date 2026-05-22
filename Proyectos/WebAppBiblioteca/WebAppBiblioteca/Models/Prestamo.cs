using WebAppBiblioteca.Enums;

namespace WebAppBiblioteca.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        public int LectorId { get; set; }
        public Lector Lector { get; set; } = new();

        public DateTime FechaPrestamo { get; set; }

        public EstadoPrestamo Estado { get; set; } = EstadoPrestamo.Activo;
    }
}
