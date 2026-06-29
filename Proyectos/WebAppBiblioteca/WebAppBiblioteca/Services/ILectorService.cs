using Microsoft.AspNetCore.Mvc;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Services
{
    public interface ILectorService
    {
        Task<IEnumerable<Lector>> ObtenerListaAsync();
        Task<Lector?> ObtenerPorIdAsync(int id);
        Task<Lector?> CrearAsync(Lector lector);
        Task<bool> ActualizarAsync(int id, Lector lector);
        Task<bool> EliminarAsync(int id);
    }
}
