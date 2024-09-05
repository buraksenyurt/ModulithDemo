namespace RentAGame.Catalog.Games.Features.GetGames;

public record GetGamesResponse(IEnumerable<GameDto> Games);

public class GetGamesEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/games", async (ISender sender) =>
        {
            var result = await sender.Send(new GetGamesQuery());
            var response = result.Adapt<GetGamesResponse>();
            return Results.Ok(response);
        })
        .WithName("GetGames")
        .Produces<GetGamesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get All Games")
        .WithDescription("Get all games order by title");
    }
}