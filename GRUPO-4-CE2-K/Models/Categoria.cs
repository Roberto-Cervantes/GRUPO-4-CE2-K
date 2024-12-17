using System.ComponentModel.DataAnnotations;

namespace GRUPO_4_CE2_K.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime RegisteredAt { get; set; } = DateTime.Now; // Se configura automáticamente
        public int RegisteredByUserId { get; set; } // FK de usuario creador
    }
}
