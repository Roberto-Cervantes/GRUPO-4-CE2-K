using GRUPO_4_CE2_K.Data;
using GRUPO_4_CE2_K.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var eventos = await _context.Eventos.Include(e => e.Categoria).ToListAsync();
            return View(eventos);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        // POST: Eventos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descripcion,CategoriaId,Fecha,Hora,Duracion,Ubicacion,CupoMaximo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.FechaRegistro = DateTime.Now;
                evento.UsuarioRegistro = User.Identity.Name;
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", evento.CategoriaId);
            return View(evento);
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

            var evento = await _context.Eventos.Include(e => e.Categoria).FirstOrDefaultAsync(m => m.Id == id);
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
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Inscripciones/Index
        public async Task<IActionResult> Inscripcion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.Include(e => e.Categoria).FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Inscripcion/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inscripcion(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                TempData["Error"] = "El evento no existe.";
                return RedirectToAction(nameof(Index));
            }

            var usuarioId = User.Identity.Name;

            // Verificar si el usuario ya está inscrito
            var inscripcionExistente = await _context.Inscripciones
                .FirstOrDefaultAsync(i => i.EventoId == id && i.UsuarioId == usuarioId);

            if (inscripcionExistente != null)
            {
                TempData["Error"] = "Ya estás inscrito en este evento.";
                return RedirectToAction(nameof(Index));
            }

            // Verificar si hay cupo disponible
            var inscripcionesActuales = await _context.Inscripciones
                .CountAsync(i => i.EventoId == id);

            if (inscripcionesActuales >= evento.CupoMaximo)
            {
                TempData["Error"] = "No hay cupos disponibles para este evento.";
                return RedirectToAction(nameof(Index));
            }

            // Crear la inscripción
            var inscripcion = new Inscripcion
            {
                EventoId = id,
                UsuarioId = usuarioId,
                FechaInscripcion = DateTime.Now
            };

            _context.Add(inscripcion);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Inscripción realizada con éxito.";
            return RedirectToAction(nameof(Index));
        }




        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.Include(e => e.Categoria).FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
