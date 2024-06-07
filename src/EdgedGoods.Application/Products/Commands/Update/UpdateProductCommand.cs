using EdgedGoods.Domain.Common.ValueObjects;
using EdgedGoods.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.Products.Commands.Update;

public record UpdateProductCommand(    
    Guid Id,
    string Name,
    Money Price,
    string? Description,
    List<Image>? Images,
    int? Quantity,
    Guid? BucketId) : IRequest<ErrorOr<ProductId>>;