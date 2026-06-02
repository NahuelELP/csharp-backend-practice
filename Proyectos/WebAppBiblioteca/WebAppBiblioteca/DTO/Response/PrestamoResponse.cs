using WebAppBiblioteca.Enums;
namespace WebAppBiblioteca.DTO.Response
{
    public class PrestamoResponse
    {
        public int PrestamoId { get; set; }
        public string NombreLector { get; set; } = string.Empty;
        public DateTime FechaDelPrestamo { get; set; } = DateTime.Now;
        public EstadoPrestamo Estado { get; set; }
        public List<DetallePrestamoResponse> DetallesPrestamoResponse { get; set; } = new();
    }
}
