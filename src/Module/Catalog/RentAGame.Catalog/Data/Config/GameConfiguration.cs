
namespace RentAGame.Catalog.Data.Config;

/*
    DbContext türevli CatalogDbContext dosyasında override ettiğimi OnModelCreating metodunda
    ApplyConfigurationsFromAssembly çağrısı yer alıyor. Bu çağrı Assembly içerisinde,
    IEntityTypeConfiguration<T> implementasyonlarını uygular. Böylece Entity türleri için gerekli
    bazı doğrulama kriterleri veya opsiyonlar da uygulanmış olur. 
    Kod yönetimi ve okunurluğu açısından oldukça iyi.
*/
public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        // Id isimli bir primary key olmalı
        builder.HasKey(g => g.Id);
        // Title olmalı ve maksimum 50 karakter olmalı vs
        builder.Property(g => g.Title).HasMaxLength(50).IsRequired();
        builder.Property(g => g.Genres).IsRequired();
        builder.Property(g => g.Description).HasMaxLength(300).IsRequired();
        builder.Property(g => g.ListPrice).IsRequired();
        builder.Property(g => g.ThumbnailImage).IsRequired();
    }
}