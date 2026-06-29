using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Models;
using WebAppBiblioteca.Services.AutorService;

namespace WebAppBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }
        [HttpGet("ObtenerLista")]
        public async Task<ActionResult<IEnumerable<Autor>>> ObtenerLista()
        {
            var autores = await _autorService.ObtenerListaAsync();
            return Ok(autores);
        }
        [HttpGet("ObtenerPorId/")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {
            var autorEncontrado = await _autorService.ObtenerPorIdAsync(id);
            if (autorEncontrado == null)
            {
                return NotFound();
            }
            return Ok(autorEncontrado);
        }
        [HttpPost("CrearAutor")]
        public async Task<ActionResult> CrearAutor(Autor autor)
        {
            var autorCreado = await _autorService.CrearAsync(autor);
            if (autorCreado == null)
            {
                return BadRequest("Ya existe un autor con el mismo nombre.");
            }
            return CreatedAtAction(nameof(ObtenerPorId), new { id = autorCreado.Id }, autorCreado);
        }
        [HttpPut("ActualizarAutor/{id}")]
        public async Task<ActionResult> ActualizarAutor(int id, Autor autor)
        {
            var autorActualizado = await _autorService.ActualizarAsync(id, autor);
            if (!autorActualizado)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("EliminarAutor")]
        public async Task<ActionResult> EliminarAutor(int id)
        {
            var autorEliminado = await _autorService.EliminarAsync(id);
            if (!autorEliminado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
