namespace RentAGame.Catalog.Games.Features.GetGamesByPriceRange;

public record GetGamesByPriceRangeRequest(decimal MinValue,decimal MaxValue);
public record GetGamesByPriceRangeResponse(IEnumerable<GameDto> Games);

public class GetGamesByPriceRangeEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/games/price", async (GetGamesByPriceRangeRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetGamesByPriceRangeQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetGamesByPriceRangeResponse>();
            return Results.Ok(response);
        })
        .WithName("GetGamesByPriceRange")
        .Produces<GetGamesByPriceRangeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Games by Price Range")
        .WithDescription("Get all games between min and max list prices");
    }
}