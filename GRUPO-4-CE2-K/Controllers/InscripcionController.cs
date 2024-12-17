using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GRUPO_4_CE2_K.Models;
using System.Threading.Tasks;
using System;
using GRUPO_4_CE2_K.Data;

namespace GRUPO_4_CE2_K.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InscripcionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Inscripcion
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inscripciones = await _context.Inscripcion
                .Include(i => i.Event)
                .ToListAsync();
            return Ok(inscripciones);
        }

        // GET: api/Inscripcion/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inscripcion = await _context.Inscripcion
                .Include(i => i.Event)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inscripcion == null)
                return NotFound(new { message = "Inscripción no encontrada." });

            return Ok(inscripcion);
        }

        // POST: api/Inscripcion
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inscripcion inscripcion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar existencia del evento
            var evento = await _context.Evento.FindAsync(inscripcion.EventId);
            if (evento == null)
                return BadRequest(new { message = "El evento especificado no existe." });

            // Validar que el cupo no esté lleno
            var totalInscritos = await _context.Inscripcion.CountAsync(i => i.EventId == inscripcion.EventId);
            if (totalInscritos >= evento.MaxAttendees)
                return BadRequest(new { message = "El evento ya alcanzó el cupo máximo de asistentes." });

            // Asignar fecha automática de registro
            inscripcion.RegisteredAt = DateTime.Now;

            // Guardar inscripción
            _context.Inscripcion.Add(inscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = inscripcion.Id }, inscripcion);
        }

        // DELETE: api/Inscripcion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inscripcion = await _context.Inscripcion.FindAsync(id);
            if (inscripcion == null)
                return NotFound(new { message = "Inscripción no encontrada." });

            _context.Inscripcion.Remove(inscripcion);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inscripción eliminada exitosamente." });
        }
    }
}
