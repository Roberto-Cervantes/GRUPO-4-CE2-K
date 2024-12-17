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
    public class AsistenciaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Asistencia
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var asistencias = await _context.Asistencia
                .Include(a => a.Event)
                .ToListAsync();
            return Ok(asistencias);
        }

        // GET: api/Asistencia/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var asistencia = await _context.Asistencia
                .Include(a => a.Event)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asistencia == null)
                return NotFound(new { message = "Asistencia no encontrada." });

            return Ok(asistencia);
        }

        // POST: api/Asistencia
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Asistencia asistencia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar existencia del evento
            var evento = await _context.Evento.FindAsync(asistencia.EventId);
            if (evento == null)
                return BadRequest(new { message = "El evento especificado no existe." });

            asistencia.MarkedAt = DateTime.Now; // Fecha de marcado automática
            _context.Asistencia.Add(asistencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = asistencia.Id }, asistencia);
        }

        // PUT: api/Asistencia/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Asistencia asistencia)
        {
            if (id != asistencia.Id)
                return BadRequest(new { message = "El ID proporcionado no coincide con la asistencia." });

            var existingAsistencia = await _context.Asistencia.FindAsync(id);
            if (existingAsistencia == null)
                return NotFound(new { message = "Asistencia no encontrada." });

            // Actualizar propiedades
            existingAsistencia.IsPresent = asistencia.IsPresent;
            existingAsistencia.MarkedAt = DateTime.Now; // Actualizar la fecha de marcado

            _context.Asistencia.Update(existingAsistencia);
            await _context.SaveChangesAsync();

            return Ok(existingAsistencia);
        }

        // DELETE: api/Asistencia/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var asistencia = await _context.Asistencia.FindAsync(id);
            if (asistencia == null)
                return NotFound(new { message = "Asistencia no encontrada." });

            _context.Asistencia.Remove(asistencia);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Asistencia eliminada exitosamente." });
        }
    }
}
