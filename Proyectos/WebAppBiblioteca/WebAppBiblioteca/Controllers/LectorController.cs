using Microsoft.AspNetCore.Mvc;
using WebAppBiblioteca.Models;
using WebAppBiblioteca.Services;

namespace WebAppBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectorController : ControllerBase
    {
        private readonly ILectorService _lectorService;

        public LectorController(ILectorService lectorService)
        {
            _lectorService = lectorService;
        }

        [HttpGet("ObtenerLista")]
        public async Task<ActionResult<IEnumerable<Lector>>> ObtenerLista()
        {
            var lectores = await _lectorService.ObtenerListaAsync();
            return Ok(lectores);
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {
            var lectorEncontrado = await _lectorService.ObtenerPorIdAsync(id);

            if (lectorEncontrado == null)
            {
                return NotFound();
            }

            return Ok(lectorEncontrado);
        }

        [HttpPost("CrearLector")]
        public async Task<ActionResult> CrearLector(Lector lector)
        {
            var lectorCreado = await _lectorService.CrearAsync(lector);

            if (lectorCreado == null)
            {
                return BadRequest("Ya existe un lector con el mismo correo electrónico.");
            }

            return CreatedAtAction(nameof(ObtenerPorId), new { id = lectorCreado.Id }, lectorCreado);
        }

        [HttpPut("ActualizarLector/{id}")]
        public async Task<ActionResult> ActualizarLector(int id, Lector lector)
        {
            var lectorActualizado = await _lectorService.ActualizarAsync(id, lector);

            if (!lectorActualizado)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("EliminarLector/{id}")]
        public async Task<ActionResult> EliminarLector(int id)
        {
            var lectorEliminado = await _lectorService.EliminarAsync(id);

            if (!lectorEliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}