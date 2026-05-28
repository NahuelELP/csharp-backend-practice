using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Data;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectorController : Controller
    {
        private readonly AppDbContext _context;
        public LectorController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("ObtenerLista")]
        public async Task<ActionResult<IEnumerable<Lector>>> ObtenerLista()
        {
            var lectores = await _context.Lectores.ToListAsync();
            return Ok(lectores);
        }
        [HttpGet("ObtenerPorId/{id}")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {
            var lectorEncontrado = await _context.Lectores.FindAsync(id);
            if (lectorEncontrado == null)
            {
                return NotFound();
            }
            return Ok(lectorEncontrado);
        }
        [HttpPost("CrearLector")]
        public async Task<ActionResult> CrearLector(Lector lector)
        {
            _context.Lectores.Add(lector);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerPorId), new { id = lector.Id }, lector);
        }
        [HttpPut("ActualizarLector/{id}")]
        public async Task<ActionResult> ActualizarLector(int id, Lector lector)
        {
            var lectorEncontrado = await _context.Lectores.FindAsync(id);
            if (lectorEncontrado == null)
            {
                return NotFound();
            }
            lectorEncontrado.Nombre = lector.Nombre;
            lectorEncontrado.Email = lector.Email;
            lectorEncontrado.Telefono = lector.Telefono;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("EliminarLector/{id}")]
        public async Task<ActionResult> EliminarLector(int id)
        {
            var lectorEncontrado = await _context.Lectores.FindAsync(id);
            if (lectorEncontrado == null)
            {
                return NotFound();
            }
            _context.Lectores.Remove(lectorEncontrado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
