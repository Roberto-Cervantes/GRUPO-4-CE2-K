﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GRUPO_4_CE2_K.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Evento Event { get; set; }

        [Required]
        public int UserId { get; set; } // FK de usuario registrado

        public DateTime RegisteredAt { get; set; } = DateTime.Now; // Fecha de registro
    }
}
