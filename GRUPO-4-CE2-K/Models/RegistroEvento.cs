using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GRUPO_4_CE2_K.Areas.Identity.Data;

namespace GRUPO_4_CE2_K.Models
{
    public class RegistroEvento
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
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public Usuario Usuario { get; set; }
        public Evento Evento { get; set; }
    }
}
