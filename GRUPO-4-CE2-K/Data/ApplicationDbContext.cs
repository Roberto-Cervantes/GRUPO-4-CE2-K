using GRUPO_4_CE2_K.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GRUPO_4_CE2_K.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RepairRequest> RepairRequests { get; set; } // Registrar RepairRequest
        public DbSet<RepairType> RepairTypes { get; set; } // Registrar la entidad RepairType
        public DbSet<Mechanic> Mechanics { get; set; }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        // Configuración del modelo de identidad
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Evento>()
                .HasOne(e => e.Categoria)
                .WithMany()
                .HasForeignKey(e => e.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Inscripcion>()
                .HasOne(i => i.Evento)
                .WithMany()
                .HasForeignKey(i => i.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
