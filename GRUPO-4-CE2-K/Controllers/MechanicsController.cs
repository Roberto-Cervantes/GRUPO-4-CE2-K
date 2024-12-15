using GRUPO_4_CE2_K.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GRUPO_4_CE2_K.Controllers
{
    public class MechanicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MechanicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para ver las solicitudes pendientes
        public async Task<IActionResult> PendingRequests()
        {
            var pendingRequests = await _context.RepairRequests
                .Where(r => !r.IsCompleted && r.MechanicId == null) // Solicitudes pendientes no asignadas
                .Include(r => r.RepairType) // Incluir detalles del tipo de reparación
                .ToListAsync();

            ViewBag.Mechanics = await _context.Mechanics.ToListAsync(); // Cargar mecánicos disponibles
            return View(pendingRequests); // Devuelve la vista con las solicitudes pendientes
        }

        // Acción para asignar una solicitud a un mecánico
        [HttpPost]
        public async Task<IActionResult> AssignRequest(int requestId, int mechanicId)
        {
            var request = await _context.RepairRequests.FindAsync(requestId);
            if (request != null)
            {
                request.MechanicId = mechanicId; // Asignar el mecánico
                await _context.SaveChangesAsync(); // Guardar los cambios
            }
            return RedirectToAction("PendingRequests"); // Redirigir a la lista de pendientes
        }

        // Acción para marcar una solicitud como completada
        [HttpPost]
        public async Task<IActionResult> CompleteRequest(int requestId)
        {
            var request = await _context.RepairRequests.FindAsync(requestId);
            if (request != null)
            {
                request.IsCompleted = true; // Marcar como completada
                await _context.SaveChangesAsync(); // Guardar los cambios
            }
            return RedirectToAction("PendingRequests"); // Redirigir a la lista de pendientes
        }
    }
}
