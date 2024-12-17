using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GRUPO_4_CE2_K.Models;
using System.Threading.Tasks;
using GRUPO_4_CE2_K.Data;
using Microsoft.AspNetCore.Identity;

namespace GRUPO_4_CE2_K.Controllers
{
    public class InscripcionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public InscripcionController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context; //?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager; //?? throw new ArgumentNullException(nameof(userManager));
        }
    

        #region CRUD

        // GET: Inscripcion
        public async Task<IActionResult> Index()
        {
            var inscripciones = await _context.Inscripcion
                .Include(i => i.Event)
                .ToListAsync();
            return View(inscripciones);
        }

        // GET: Inscripcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var inscripcion = await _context.Inscripcion
                .Include(i => i.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (inscripcion == null)
                return NotFound();

            return View(inscripcion);
        }

        // GET: Inscripcion/Create
        public IActionResult Create()
        {
            ViewBag.Events = _context.Evento.ToList(); // Lista de eventos

            var usuarios = _userManager.Users.ToList();
            ViewBag.Users = usuarios.Any() ? usuarios : new List<IdentityUser>();

            return View();
        }

        // POST: Inscripcion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,UserId")] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                inscripcion.RegisteredAt = DateTime.Now; // Fecha automática
                _context.Add(inscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Events = _context.Evento.ToList();
            return View(inscripcion);
        }

        // GET: Inscripcion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var inscripcion = await _context.Inscripcion.FindAsync(id);
            if (inscripcion == null)
                return NotFound();

            ViewBag.Events = _context.Evento.ToList();
            return View(inscripcion);
        }

        // POST: Inscripcion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,UserId")] Inscripcion inscripcion)
        {
            if (id != inscripcion.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    inscripcion.RegisteredAt = DateTime.Now; // Actualizar fecha de registro
                    _context.Update(inscripcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcionExists(inscripcion.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Events = _context.Evento.ToList();
            return View(inscripcion);
        }

        // GET: Inscripcion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var inscripcion = await _context.Inscripcion
                .Include(i => i.Event)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (inscripcion == null)
                return NotFound();

            return View(inscripcion);
        }

        // POST: Inscripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcion = await _context.Inscripcion.FindAsync(id);

            if (inscripcion != null)
            {
                _context.Inscripcion.Remove(inscripcion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InscripcionExists(int id)
        {
            return _context.Inscripcion.Any(e => e.Id == id);
        }

        #endregion
    }
}
