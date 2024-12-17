using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GRUPO_4_CE2_K.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Categoria Category { get; set; } // Propiedad de navegación

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public TimeSpan EventTime { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser mayor que 0.")]
        public int DurationMinutes { get; set; }

        [Required]
        public string Location { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El cupo máximo debe ser mayor que 0.")]
        public int MaxAttendees { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now; // Se configura automáticamente
        public int RegisteredByUserId { get; set; } // FK de usuario creador
    }
}
