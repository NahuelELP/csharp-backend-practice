using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Data;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Services.AutorService
{
    public class AutorService : IAutorService
    {
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Autor>> ObtenerListaAsync()
        {
            return await _context.Autores.ToListAsync();
        }

        public async Task<Autor?> ObtenerPorIdAsync(int id)
        {
            return await _context.Autores.FindAsync(id);
        }
        public async Task<Autor?> CrearAsync(Autor autor)
        {
            var autorExistente = await _context.Autores.FirstOrDefaultAsync(a => a.Nombre == autor.Nombre);
            if (autorExistente != null)
            {
                return null; // Autor con el mismo nombre ya existe
            }
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return autor;
        }
        public async Task<bool> ActualizarAsync(int id, Autor autor)
        {
            var autorEncontrado = await _context.Autores.FindAsync(id);
            if (autorEncontrado == null)
            {
                return false; // Autor no encontrado
            }
            autorEncontrado.Nombre = autor.Nombre;
            _context.Autores.Update(autorEncontrado);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarAsync(int id)
        {
            var autorEncontrado = await _context.Autores.FindAsync(id);
            if (autorEncontrado == null)
            {
                return false; // Autor no encontrado
            }
            _context.Autores.Remove(autorEncontrado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
