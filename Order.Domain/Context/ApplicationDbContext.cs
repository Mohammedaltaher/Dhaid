using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Context;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) => Database.Migrate();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        MapModelBuilder(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
    private static void MapModelBuilder(ModelBuilder modelBuilder) => modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsDeleted);
    public async Task<int> SaveChangesAsync()
    {
        UpdateUpdateDate();
        return await base.SaveChangesAsync();
    }

    private void UpdateUpdateDate()
    {
        var updateDate = "UpdateDate";
        ChangeTracker.DetectChanges();
        var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (var entity in modified)
        {
            foreach (var prop in entity.Properties)
            {
                if (prop.Metadata.Name == updateDate)
                {
                    entity.CurrentValues[updateDate] = DateTime.Now;
                }
            }
        }
    }

    #region DbSet Properties
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    #endregion
}

