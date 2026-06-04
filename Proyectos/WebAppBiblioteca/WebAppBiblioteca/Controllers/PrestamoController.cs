using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Data;
using WebAppBiblioteca.DTO.Request;
using WebAppBiblioteca.DTO.Response;
using WebAppBiblioteca.Models;
using WebAppBiblioteca.Enums;

namespace WebAppBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        readonly AppDbContext _context;
        public PrestamoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("CrearPrestamo")]
        public async Task<ActionResult<PrestamoResponse>> CrearPrestamo(PrestamoRequest prestamoRequest)
        {
            //Validar que el lector exista
            var lectorEncontrado = await _context.Lectores.FindAsync(prestamoRequest.LectorId);
            if (lectorEncontrado == null)
            {
                return NotFound();
            }
            //validar que el préstamo tenga al menos un libro
            if (prestamoRequest.DetallePrestamoRequests == null || !prestamoRequest.DetallePrestamoRequests.Any())
            {
                return BadRequest("El préstamo debe tener al menos un libro.");
            }
            //Crear el préstamo
            var prestamo = new Prestamo
            {
                LectorId = prestamoRequest.LectorId,
                FechaPrestamo = DateTime.Now,
                Estado = EstadoPrestamo.Activo
            };
            //Guardar el préstamo para obtener su Id
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            //Crear los detalles del préstamo
            var detallesResponse = new List<DetallePrestamoResponse>();
            //recorrer cada detalle del préstamo, validar que el libro exista y tenga stock
            //crear el detalle del préstamo y actualizar el stock del libro
            foreach (var detalleRequest in prestamoRequest.DetallePrestamoRequests)
            {
                var libroEncontrado = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == detalleRequest.LibroId);
                if (libroEncontrado == null)
                {
                    return NotFound($"El libro con Id {detalleRequest.LibroId} no existe.");
                }

                if (libroEncontrado.Stock <= 0)
                {
                    return BadRequest($"El libro '{libroEncontrado.Titulo}' no tiene stock disponible.");
                }
                //Crear el detalle del préstamo
                var detallePrestamo = new DetallePrestamo
                {
                    PrestamoId = prestamo.Id,
                    LibroId = libroEncontrado.Id
                };
                _context.DetallesPrestamo.Add(detallePrestamo);

                libroEncontrado.Stock--;
                //Agregar el detalle del préstamo a la respuesta
                detallesResponse.Add(new DetallePrestamoResponse
                {
                    LibroId = libroEncontrado.Id,
                    Titulo = libroEncontrado.Titulo,
                    Autor = libroEncontrado.Autor.Nombre
                });
            }
            await _context.SaveChangesAsync();
            //Crear la respuesta del préstamo
            var prestamoResponse = new PrestamoResponse
            {
                PrestamoId = prestamo.Id,
                NombreLector = lectorEncontrado.Nombre,
                FechaDelPrestamo = prestamo.FechaPrestamo,
                Estado = prestamo.Estado,
                DetallesPrestamoResponse = detallesResponse
            };
            //Devolver la respuesta del préstamo
            return Ok(prestamoResponse);//createdAtAction(nameof(ObtenerPrestamoPorId), new { id = prestamo.Id }, prestamoResponse);
        }
        [HttpGet("ObtenerPrestamoPorId/{id}")]
        public async Task<ActionResult<PrestamoResponse>> ObtenerPrestamoPorId(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Lector)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }
            var detalles = await _context.DetallesPrestamo
               .Where(d => d.PrestamoId == prestamo.Id)
               .Include(d => d.Libro)
               .ThenInclude(l => l.Autor)
               .ToListAsync();

            var prestamoResponse = new PrestamoResponse
            {
                PrestamoId = prestamo.Id,
                NombreLector = prestamo.Lector.Nombre,
                FechaDelPrestamo = prestamo.FechaPrestamo,
                Estado = prestamo.Estado,
                DetallesPrestamoResponse = detalles.Select(d => new DetallePrestamoResponse
                {
                    LibroId = d.LibroId,
                    Titulo = d.Libro.Titulo,
                    Autor = d.Libro.Autor.Nombre
                }).ToList()
            };

            return Ok(prestamoResponse);
        }
    }
}