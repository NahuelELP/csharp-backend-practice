using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAppBiblioteca.Data;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Services.LibroService
{
    public class LibroService : ILibroService
    {
        private readonly AppDbContext _context;
        public LibroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ActualizarAsync(int id, Libro libro)
        {
            var libroEncontrado = await _context.Libros.FindAsync(id);
            if (libroEncontrado == null)
            {
                return false; // Libro no encontrado
            }
            libroEncontrado.Titulo = libro.Titulo;
            libroEncontrado.Autor = libro.Autor;
            _context.Libros.Update(libroEncontrado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Libro?> CrearAsync(Libro libro)
        {
            var libroExistente = await _context.Libros.FirstOrDefaultAsync(l => l.Titulo == libro.Titulo);
            if (libroExistente != null)
            {
                return null; // Libro con el mismo título ya existe
            }
            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();
            return libro;
        }

        public async Task<bool> EliminarLibro(int id)
        {
            var libroEncontrado = await _context.Libros.FindAsync(id);
            if (libroEncontrado == null)
            {
                return false; // Libro no encontrado
            }
            _context.Libros.Remove(libroEncontrado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Libro>> ObtenerAsync()
        {
            var libros = await _context.Libros.ToListAsync();
            return libros;
        }

        public async Task<Libro?> ObtenerPorIdAstync(int id)
        {
           var libroEncontrado = await _context.Libros.FindAsync(id);
            if (libroEncontrado == null)
            {
                return null;
            }
            return libroEncontrado;
        }
    }
}
