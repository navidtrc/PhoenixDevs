using Framework.Core.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Phoenix.Domain.Aggregates.Subscription;
using Phoenix.Domain.Aggregates.User;
using Phoenix.Infrastructure.Extensions;
using System.Reflection;
using Utilities.Extensions;

namespace Phoenix.Infrastructure.Contexts;

public class ApplicationDbContextReadOnly(DbContextOptions<ApplicationDbContextReadOnly> options) : DbContext(options)
{

    public DbSet<User> Users { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.HasDefaultSchema("Phoenix");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContextReadOnly).Assembly);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();

        base.ConfigureConventions(configurationBuilder);
    }
    public override int SaveChanges()
    {
        throw new NotSupportedException();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new NotSupportedException();

    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }
}

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{

    public DbSet<User> Users { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Phoenix");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();

        base.ConfigureConventions(configurationBuilder);
    }
    public override int SaveChanges()
    {
        _cleanString();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        _cleanString();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        _cleanString();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _cleanString();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void _cleanString()
    {
        var changedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
        foreach (var item in changedEntities)
        {
            if (item.Entity == null)
                continue;

            var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                var val = (string)property.GetValue(item.Entity, null);

                if (val.HasValue())
                {
                    var newVal = val.Fa2En().FixPersianChars();
                    if (newVal == val)
                        continue;

                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }


}
public static class ApplicationDbContextSchema
{
    #region DATABASE
    public const string EF_MIGRATION_TABLE_NAME = "__EFMigrationsHistory";
    public const string EF_Decimal_Type = "decimal(18, 2)";
    public const int EF_NvarCharMax_Length = 2000;
    public const string EF_DateTime_Type = "datetime";

    public static readonly (string Default, string ReadOnly) ConnectionString = ("DefaultConnection", "DefaultReadOnlyConnection");
    #endregion
}
