namespace RentAGame.Catalog.Games.Features.GetGameById;

public record GetGameByIdResponse(GameDto Game);

public class GetGameByIdEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/games/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetGameByIdQuery(id));
            var response = result.Adapt<GetGameByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetGameById")
        .Produces<GetGameByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Game by Id")
        .WithDescription("Get game informations by id");
    }
}