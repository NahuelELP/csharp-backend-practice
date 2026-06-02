using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.DTO.Request
{
    public class PrestamoRequest
    {
        public int LectorId { get; set; }
        public List<DetallePrestamoRequest> DetallePrestamoRequests { get; set; } = new();
    }
}
