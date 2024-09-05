namespace RentAGame.Catalog.Games.Features.DeleteGame;

public record DeleteGameResponse(bool IsSuccess);

public class DeleteGameEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/games/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteGameCommand(id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteGameResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteGame")
        .Produces<DeleteGameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Game")
        .WithDescription("Delete the game by Id");
    }
}