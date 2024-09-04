namespace RentAGame.Catalog.Games.Dtos;

public record GameDto(
    Guid Id,
    string Title,
    string Description,
    List<string> Programmers,
    string ThumbnailImage,
    decimal ListPrice,
    List<Genre> Genres
);