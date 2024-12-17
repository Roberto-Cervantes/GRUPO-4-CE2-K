using GRUPO_4_CE2_K.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GRUPO_4_CE2_K.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Inscripcion> Inscripcion { get; set; }
        public DbSet<Asistencia> Asistencia { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): 
        base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Restricción de registro único por usuario-evento
            modelBuilder.Entity<Inscripcion>()
                .HasIndex(e => new { e.EventId, e.UserId })
                .IsUnique();

            // Relaciones
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Event)
                .WithMany()
                .HasForeignKey(a => a.EventId);
        }

    }
}
