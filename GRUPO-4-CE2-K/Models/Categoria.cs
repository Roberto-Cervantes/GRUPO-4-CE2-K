using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GRUPO_4_CE2_K.Areas.Identity.Data;

namespace GRUPO_4_CE2_K.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("Usuario")]
        public int RegisteredByUserId { get; set; }

        // Navigation Properties
        public Usuario RegisteredByUser { get; set; }
        public ICollection<Evento> Eventos { get; set; }
    }
}
