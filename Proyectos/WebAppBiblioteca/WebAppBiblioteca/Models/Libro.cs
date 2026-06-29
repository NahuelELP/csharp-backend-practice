namespace WebAppBiblioteca.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public DateTime AñoPublicado { get; set; } // cambiar data
        public int Stock { get; set; }

        public int AutorId { get; set; }
        public Autor Autor { get; set; } = new();
    }
}
