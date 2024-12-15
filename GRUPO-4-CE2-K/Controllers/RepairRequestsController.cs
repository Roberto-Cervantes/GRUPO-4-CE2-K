using Microsoft.AspNetCore.Mvc;
using GRUPO_4_CE2_K.Data;
using GRUPO_4_CE2_K.Models;
using Microsoft.AspNetCore.Identity;

namespace GRUPO_4_CE2_K.Controllers
{
    public class RepairRequestsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager; // Agrega esta variable
        private readonly ApplicationDbContext _context;

        public RepairRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.RepairTypes = _context.RepairTypes.ToList();
            // Cargar tipos de reparación

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RepairRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.RepairRequests.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RepairTypes = _context.RepairTypes.ToList(); // Re-cargar tipos en caso de error
            return View(request);
        }
    }

}
