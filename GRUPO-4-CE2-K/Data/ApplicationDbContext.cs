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
    }
}
