using Microsoft.AspNetCore.Mvc;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Services.LibroService
{
    public interface ILibroService
    {
        Task<IEnumerable<Libro>> ObtenerAsync();
        Task<Libro?> ObtenerPorIdAstync(int id);
        Task<Libro?> CrearAsync(Libro libro);
        Task<bool> ActualizarAsync(int id, Libro libro);
        Task<bool> EliminarLibro(int id);
    }
}
