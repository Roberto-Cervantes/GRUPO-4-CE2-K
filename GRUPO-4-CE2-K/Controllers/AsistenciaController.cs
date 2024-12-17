using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GRUPO_4_CE2_K.Models;
using System.Threading.Tasks;
using GRUPO_4_CE2_K.Data;

namespace GRUPO_4_CE2_K.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region CRUD

        // GET: Asistencia
        public async Task<IActionResult> Index()
        {
            var asistencias = await _context.Asistencia
                .Include(a => a.Event)
                .ToListAsync();
            return View(asistencias);
        }

        // GET: Asistencia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var asistencia = await _context.Asistencia
                .Include(a => a.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (asistencia == null)
                return NotFound();

            return View(asistencia);
        }

        // GET: Asistencia/Create
        public IActionResult Create()
        {
            ViewBag.Events = _context.Evento.ToList(); // Cargar lista de eventos
            return View();
        }

        // POST: Asistencia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,UserId,IsPresent")] Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                asistencia.MarkedAt = DateTime.Now; // Fecha automática
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Events = _context.Evento.ToList(); // Recargar eventos si falla
            return View(asistencia);
        }

        // GET: Asistencia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var asistencia = await _context.Asistencia.FindAsync(id);
            if (asistencia == null)
                return NotFound();

            ViewBag.Events = _context.Evento.ToList(); // Cargar lista de eventos
            return View(asistencia);
        }

        // POST: Asistencia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,UserId,IsPresent")] Asistencia asistencia)
        {
            if (id != asistencia.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    asistencia.MarkedAt = DateTime.Now; // Actualizar la fecha
                    _context.Update(asistencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(asistencia.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Events = _context.Evento.ToList();
            return View(asistencia);
        }

        // GET: Asistencia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var asistencia = await _context.Asistencia
                .Include(a => a.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (asistencia == null)
                return NotFound();

            return View(asistencia);
        }

        // POST: Asistencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistencia = await _context.Asistencia.FindAsync(id);
            if (asistencia != null)
            {
                _context.Asistencia.Remove(asistencia);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciaExists(int id)
        {
            return _context.Asistencia.Any(e => e.Id == id);
        }

        #endregion
    }
}
