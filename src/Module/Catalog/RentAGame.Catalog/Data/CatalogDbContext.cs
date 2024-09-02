namespace RentAGame.Catalog.Data;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Game> Games => Set<Game>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Postgresql veritabanında Catalog modülü için catalog isimli şemanın kullanılacağını belirtiyoruz.
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}