namespace RentAGame.Catalog.Games.Models;

// Rich Domain Model Entity türünden örnek
// Anemic Entity türü sadece getter, setter ile bezenmiş property'ler içerir. 
public class Game
    : Aggeregate<Guid> // Böylece Domain içerisine Event' te fırlatabileceğiz
{
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public List<string> Programmers { get; private set; } = [];
    public decimal ListPrice { get; private set; }
    public string ThumbnailImage { get; private set; } = default!;
    public List<Genre> Genres { get; private set; } = [];

    public static Game Create(Guid id, string title, string description, List<string> programmers, decimal listPrice, string thumbnailImage, List<Genre> genres)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);
        ArgumentException.ThrowIfNullOrEmpty(description);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(listPrice);

        var game = new Game
        {
            Id = id,
            Title = title,
            Description = description,
            Programmers = programmers,
            ListPrice = listPrice,
            ThumbnailImage = thumbnailImage,
            Genres = genres
        };

        // Yeni bir oyun nesnesi oluşturulduğunda domain içerisinde ele alınacak şekilde
        // yeni bir olay bilgisi de yayınlanır.

        game.AddEvent(new GameCreatedEvent(game));

        return game;
    }

    public void Update(string title, string description, List<string> programmers, decimal listPrice, string thumbnailImage, List<Genre> genres)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);
        ArgumentException.ThrowIfNullOrEmpty(description);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(listPrice);

        Title = title;
        Description = description;
        Programmers = programmers;
        ListPrice = listPrice;
        ThumbnailImage = thumbnailImage;
        Genres = genres;

        if (ListPrice != listPrice)
        {
            AddEvent(new GameListPriceChangedEvent(this));
        }
    }
}