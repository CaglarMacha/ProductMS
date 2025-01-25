using Microsoft.EntityFrameworkCore;
using PSM.Domain;
using PSM.Domain.AuditLogging;
using PSM.Domain.Categories;
using PSM.Domain.Entities;
using PSM.Domain.Products;
using PSM.Domain.Stocks;
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
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<AuditLogging> AuditLoggings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasIndex(p => new { p.Title, p.IsDeleted, p.IsActive }).IsUnique();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();

                entity.HasMany(e => e.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId);
                entity.HasIndex(p => new { p.Name, p.IsDeleted }).IsUnique();
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasOne(s => s.Product)
               .WithMany(p => p.Stocks)
               .HasForeignKey(s => s.ProductId);
            });

            modelBuilder.Entity<AuditLog>().HasKey(p => p.Id);
        }
        public override int SaveChanges()
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges();
            OnAfterSaveChangesAsync(auditEntries);
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChangesAsync(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLogging || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString()
                };

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (entry.State == EntityState.Added)
                    {
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        if (property.IsModified)
                        {
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                    }
                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }
        private async Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0) return;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.NewValues)
                {
                    var log = new AuditLogging
                    {
                        TableName = auditEntry.TableName,
                        Action = auditEntry.Action,
                        PrimaryKey = auditEntry.PrimaryKey,
                        ColumnName = prop.Key,
                        OldValue = auditEntry.OldValues.ContainsKey(prop.Key) ? auditEntry.OldValues[prop.Key]?.ToString() : null,
                        NewValue = prop.Value?.ToString(),
                        ChangeTime = DateTime.UtcNow,
                        ChangedBy = "system"
                    };

                    AuditLoggings.Add(log);
                }
            }

            await SaveChangesAsync();
        }
    }
}
