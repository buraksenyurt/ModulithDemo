using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RentAGame.Catalog.Data;

/*
    Bir Entity veritabanına kaydedilmeden önce araya girip bazı şeyler yapılabilir.
    Bunun için uygun Interceptor sınıfından türetmek (Örneğin SaveChangesInterceptor gibi)
    ve ilgili fonksiyonları override etmek yeterlidir.

    Aşağıdaki senaryoda Entity kaydedilmeden önce araya girilir, nesnenin durum bilgisine bakılır
    ve Entity base sınıfında kullandığımız bazı property'lerin değerleri eklenir.

    Böylece bir Entity nesnemiz kaydedilirken eğer ilk kez oluşturuluyorsa ekleyen ve eklenme zamanı bilgileri,
    eğer var olan entity güncelleniyorsa da güncelleyen ve güncelleme zamanı bilgileri bu aşamada araya girilerek verilebilir.
*/
public class EntityUpsertInterceptor 
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntity(DbContext? dbContext)
    {
        if (dbContext == null)
            return;

        foreach (var entry in dbContext.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "System";
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasAnyChange())
            {
                entry.Entity.ModifiedBy = "System";
                entry.Entity.ModifiedAt = DateTime.UtcNow;
            }
        }
    }
}

public static class EntityExtensions
{
    public static bool HasAnyChange(this EntityEntry entityEntry)
    {
        return entityEntry
            .References
            .Any(
                r => r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified)
            );
    }
}