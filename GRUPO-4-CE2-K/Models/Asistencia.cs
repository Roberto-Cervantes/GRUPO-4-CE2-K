using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GRUPO_4_CE2_K.Models
{
    public class Asistencia
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Evento Event { get; set; }

        [Required]
        public int UserId { get; set; } // FK del usuario asistente

        public bool IsPresent { get; set; } = false; // Estado de asistencia
        public DateTime MarkedAt { get; set; } = DateTime.Now; // Fecha y hora de marcado
    }
}
