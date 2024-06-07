using MediatR;
using ErrorOr;

namespace EdgedGoods.Application.StockItems.Commands.Create;

public record CreateStockItemCommand(
    Guid ProductId,
    Guid StockId,
    int Quantity,
    Guid? BucketId = null) : IRequest<ErrorOr<Success>>;