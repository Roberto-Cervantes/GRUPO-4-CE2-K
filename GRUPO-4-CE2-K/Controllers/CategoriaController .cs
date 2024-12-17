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
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _context.Categoria.ToListAsync();
            return Ok(categorias);
        }

        // GET: api/Categoria/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada." });

            return Ok(categoria);
        }

        // POST: api/Categoria
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            categoria.RegisteredAt = DateTime.Now; // Fecha de registro automática

            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        // PUT: api/Categoria/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest(new { message = "El ID de la URL no coincide con el ID del cuerpo." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCategoria = await _context.Categoria.FindAsync(id);
            if (existingCategoria == null)
                return NotFound(new { message = "Categoría no encontrada." });

            // Actualizar valores
            existingCategoria.Name = categoria.Name;
            existingCategoria.Description = categoria.Description;
            existingCategoria.IsActive = categoria.IsActive;

            _context.Categoria.Update(existingCategoria);
            await _context.SaveChangesAsync();

            return Ok(existingCategoria);
        }

        // DELETE: api/Categoria/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada." });

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Categoría eliminada exitosamente." });
        }
    }
}
