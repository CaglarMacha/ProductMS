using Microsoft.EntityFrameworkCore;
using PSM.Domain;
using PSM.Domain.Categories;
using PSM.Domain.Products;
using System.Security.AccessControl;


namespace PMS.EntityFrameworkCore.Core
{
    public class PMSDbContext : DbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id); 

                entity.Property(e => e.Name).IsRequired();

                entity.HasMany(e => e.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId);
            });
            modelBuilder.Entity<AuditLog>().HasKey(p => p.Id);
        }
    }
}
