using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBiblioteca.Data;
using WebAppBiblioteca.Models;

namespace WebAppBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AutorController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("ObtenerLista")]
        public async Task<ActionResult<IEnumerable<Autor>>> ObtenerLista()
        {
            var autores = await _context.Autores.ToListAsync();
            return Ok(autores);
        }
        [HttpGet("ObtenerPorId/")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {
            var autorEncontrado= await _context.Autores.FindAsync(id);
            if(autorEncontrado == null)
            {
                return NotFound();
            }
            return Ok(autorEncontrado);
        }
    }
}
