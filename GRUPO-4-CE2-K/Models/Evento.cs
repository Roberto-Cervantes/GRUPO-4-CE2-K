using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GRUPO_4_CE2_K.Areas.Identity.Data;

namespace GRUPO_4_CE2_K.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public int CategoryId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public TimeSpan EventTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int DurationMinutes { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxCapacity { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("Usuario")]
        public int RegisteredByUserId { get; set; }

        // Navigation Properties
        public Categoria Categoria { get; set; }
        public Usuario RegisteredByUser { get; set; }
        public ICollection<RegistroEvento> RegistroEventos { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; }
    }
}
