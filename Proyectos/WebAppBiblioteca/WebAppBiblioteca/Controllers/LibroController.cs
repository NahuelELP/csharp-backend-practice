using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Services.LibroService;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;
        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }
        [HttpGet("ObtenerLista")]
        public async Task<ActionResult<IEnumerable<Libro>>> ObtenerLista()
        {
            var libros = await _libroService.ObtenerAsync();
            return Ok(libros);
        }
        [HttpGet("ObtenerPorId/{id}")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {
            var libroEncontrado = await _libroService.ObtenerPorIdAstync(id);
            if (libroEncontrado == null)
            {
                return NotFound();
            }
            return Ok(libroEncontrado);
        }
        [HttpPost("CrearLibro")]
        public async Task<ActionResult> CrearLibro(Libro libro)
        {
            await _libroService.CrearAsync(libro);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = libro.Id }, libro);
        }
        [HttpPut("ActualizarLibro/{id}")]
        public async Task<ActionResult> ActualizarLibro(int id, Libro libro)
        {
            var libroEncontrado = await _libroService.ActualizarAsync(id, libro);
            if (!libroEncontrado)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("EliminarLibro/{id}")]
        public async Task<ActionResult> EliminarLibro(int id)
        {
            var libroEncontrado = await _libroService.EliminarLibro(id);
            if (!libroEncontrado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
