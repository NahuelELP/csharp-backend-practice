using Microsoft.AspNetCore.Mvc;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Services.AutorService
{
    public interface IAutorService
    {
        Task<IEnumerable<Autor>> ObtenerListaAsync();
        Task<Autor?> ObtenerPorIdAsync(int id);
        Task<Autor?> CrearAsync(Autor autor);
        Task<bool> ActualizarAsync(int id, Autor autor);
        Task<bool> EliminarAsync(int id);
    }
}
