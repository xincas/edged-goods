using EdgedGoods.Api.Endpoints.Common;
using EdgedGoods.Application.StockItems.Commands.Create;
using EdgedGoods.Contracts.StockItem;
using MediatR;

namespace EdgedGoods.Api.Endpoints;

public static class StockItemEndpoints
{
    public static IEndpointRouteBuilder MapStockItemEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/stock-item");

        group.MapPost("", Create)
            .WithDescription("Create new stock item");

        return builder;
    }

    private static async Task<IResult> Create(CreateStockItemRequest request, ISender sender)
    {
        var command = new CreateStockItemCommand(
            request.ProductId,
            request.StockId,
            request.Quantity,
            request.BucketId);

        var result = await sender.Send(command);

        return result.Match(
            TypedResults.Ok,
            ProblemGeneration.Problem);
    }
}