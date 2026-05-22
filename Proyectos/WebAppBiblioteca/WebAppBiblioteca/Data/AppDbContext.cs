using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Lector> Lectores { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<DetallePrestamo> DetallesPrestamo { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
