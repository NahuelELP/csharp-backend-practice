using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Data;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        readonly AppDbContext _context;
        public LibroController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("ObtenerLista")]
        public async Task<ActionResult<IEnumerable<Libro>>> ObtenerLista()
        {
            var libros = await _context.Libros.ToListAsync();
            return Ok(libros);
        }
        [HttpGet("ObtenerPorId/{id}")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {
            var libroEncontrado = await _context.Libros.FindAsync(id);
            if (libroEncontrado == null)
            {
                return NotFound();
            }
            return Ok(libroEncontrado);
        }
        [HttpPost("CrearLibro")]
        public async Task<ActionResult> CrearLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerPorId), new { id = libro.Id }, libro);
        }
        [HttpPut("ActualizarLibro/{id}")]
        public async Task<ActionResult> ActualizarLibro(int id, Libro libro)
        {
            var libroEncontrado = await _context.Libros.FindAsync(id);
            if (libroEncontrado == null)
            {
                return NotFound();
            }
            libroEncontrado.Titulo = libro.Titulo;
            libroEncontrado.Autor = libro.Autor;
            libroEncontrado.AñoPublicado = libro.AñoPublicado;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("EliminarLibro/{id}")]
        public async Task<ActionResult> EliminarLibro(int id)
        {
            var libroEncontrado = await _context.Libros.FindAsync(id);
            if (libroEncontrado == null)
            {
                return NotFound();
            }
            _context.Libros.Remove(libroEncontrado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
