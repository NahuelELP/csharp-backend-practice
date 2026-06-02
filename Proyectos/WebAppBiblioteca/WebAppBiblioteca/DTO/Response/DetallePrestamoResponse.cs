namespace WebAppBiblioteca.DTO.Response
{
    public class DetallePrestamoResponse
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
    }
}
