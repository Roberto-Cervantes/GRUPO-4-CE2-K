using GRUPO_4_CE2_K.Areas.Identity.Data;
using GRUPO_4_CE2_K.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRUPO_4_CE2_K.Controllers
{
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var eventos = await _context.Eventos.Include(e => e.Categoria).ToListAsync();
            return View(eventos);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            // Cargar las categorías desde la base de datos para el combo de selección
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", evento.CategoriaId);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,CategoriaId,Fecha,Hora,Duracion,Ubicacion,CupoMaximo")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", evento.CategoriaId);
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Inscripcion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            // Mostrar la vista de inscripción con el evento
            return View(evento);
        }

        // POST: Eventos/Inscripcion/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscripcion(int id, [Bind("Id")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            // Aquí puedes agregar la lógica para inscribir al usuario en el evento
            // Esto puede incluir la creación de una nueva inscripción, actualizando cupos disponibles, etc.

            // Ejemplo de creación de una inscripción
            var inscripcion = new Inscripcion
            {
                EventoId = evento.Id,
                UsuarioId = "UsuarioPrueba",  // Aquí debes tomar el usuario logueado
                FechaInscripcion = DateTime.Now
            };

            _context.Add(inscripcion);
            await _context.SaveChangesAsync();

            // Redirigir al Index o alguna otra vista después de la inscripción
            return RedirectToAction(nameof(Index));
        }
    }
}
