using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Data;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Services
{
    public class LectorService : ILectorService
    {
        private readonly AppDbContext _context;

        public LectorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lector>> ObtenerListaAsync()
        {
            return await _context.Lectores.ToListAsync();
        }

        public async Task<Lector?> ObtenerPorIdAsync(int id)
        {
            return await _context.Lectores.FindAsync(id);
        }
        public async Task<Lector?> CrearAsync(Lector lector)
        {
            var lectorExistente = await _context.Lectores.FirstOrDefaultAsync(l => l.Email == lector.Email);
            if (lectorExistente != null)
            {
                return null;
            }
            _context.Lectores.Add(lector);
            await _context.SaveChangesAsync();
            return lector;
        }
        public async Task<bool> ActualizarAsync(int id, Lector lector)
        {
            var lectorEncontrado = await _context.Lectores.FindAsync(id);

            if (lectorEncontrado == null)
            {
                return false;
            }

            lectorEncontrado.Nombre = lector.Nombre;
            lectorEncontrado.Email = lector.Email;
            lectorEncontrado.Telefono = lector.Telefono;
            _context.Lectores.Update(lectorEncontrado);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> EliminarAsync(int id)
        {
            var lectorEncontrado = await _context.Lectores.FindAsync(id);
            if (lectorEncontrado == null)
            {
                return false;
            }
            _context.Lectores.Remove(lectorEncontrado);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
