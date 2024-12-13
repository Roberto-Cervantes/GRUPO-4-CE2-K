using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GRUPO_4_CE2_K.Areas.Identity.Data;

namespace GRUPO_4_CE2_K.Models
{
    public class Asistencia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Evento")]
        public int EventId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; } = DateTime.Now;

        [Required]
        public bool IsPresent { get; set; } = false;

        // Navigation Properties
        public Usuario Usuario { get; set; }
        public Evento Evento { get; set; }
    }
}
