using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Entities;

namespace ShoesDb2026.Data
{
    public class ShoesDbContext : DbContext
    {
        public DbSet<SportShoe> Shoes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<SiZe> Sizes { get; set; }
        public DbSet<Sport> Sports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
               .UseSqlServer("Data Source=.; Initial Catalog=ShoesDb2026; Trusted_Connection=true; TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoesDbContext).Assembly);
            // 👇 Desactivar cascade delete globalmente
            foreach (var fk in modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
