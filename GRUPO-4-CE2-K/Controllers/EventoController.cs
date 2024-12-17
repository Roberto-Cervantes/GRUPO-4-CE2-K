using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GRUPO_4_CE2_K.Models;
using System.Threading.Tasks;
using System;
using GRUPO_4_CE2_K.Data;

namespace GRUPO_4_CE2_K.Controllers
{
    public class EventoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            var eventos = await _context.Evento
                .Include(e => e.Category)
                .ToListAsync();
            return View(eventos);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categoria.ToList(); // Lista de categorías
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Evento evento)
        {
            if (ModelState.IsValid)
            {
                // Validar que la fecha no sea en el pasado
                if (evento.EventDate < DateTime.Now)
                {
                    ModelState.AddModelError("EventDate", "La fecha no puede estar en el pasado.");
                    ViewBag.Categories = _context.Categoria.ToList();
                    return View(evento);
                }

                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categoria.ToList();
            return View(evento);
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null) return NotFound();

            ViewBag.Categories = _context.Categoria.ToList();
            return View(evento);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            if (id != evento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Validación adicional
                    if (evento.EventDate < DateTime.Now)
                    {
                        ModelState.AddModelError("EventDate", "La fecha no puede estar en el pasado.");
                        ViewBag.Categories = _context.Categoria.ToList();
                        return View(evento);
                    }

                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categoria.ToList();
            return View(evento);
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _context.Evento
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null) return NotFound();

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            if (evento != null)
            {
                _context.Evento.Remove(evento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.Id == id);
        }
    }
}
