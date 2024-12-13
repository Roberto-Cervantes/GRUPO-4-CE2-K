using GRUPO_4_CE2_K.Areas.Identity.Data;
using GRUPO_4_CE2_K.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GRUPO_4_CE2_K.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InscripcionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Inscripciones/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(int eventoId)
        {
            var usuarioId = User.Identity.Name; // Obtener el usuario actual

            // Verificar si el usuario ya está inscrito en un evento que se superpone
            var eventosSuperpuestos = await _context.Inscripciones
                .Where(i => i.UsuarioId == usuarioId &&
                            i.Evento.Fecha == _context.Eventos.Find(eventoId).Fecha &&
                            i.Evento.Hora < _context.Eventos.Find(eventoId).Hora.Add(new TimeSpan(0, _context.Eventos.Find(eventoId).Duracion, 0)))
                .ToListAsync();

            if (eventosSuperpuestos.Any())
            {
                ModelState.AddModelError("", "No puedes registrarte en dos eventos que se superpongan.");
                return View();
            }

            // Verificar si el evento tiene cupo
            var evento = await _context.Eventos.FindAsync(eventoId);
            var inscripcionesEvento = await _context.Inscripciones.CountAsync(i => i.EventoId == eventoId);

            if (inscripcionesEvento >= evento.CupoMaximo)
            {
                ModelState.AddModelError("", "El cupo del evento está lleno.");
                return View();
            }

            var inscripcion = new Inscripcion
            {
                EventoId = eventoId,
                UsuarioId = usuarioId,
                FechaInscripcion = DateTime.Now
            };

            _context.Add(inscripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Eventos");
        }
    }
}
