using EdgedGoods.Contracts.Common;

namespace EdgedGoods.Contracts.Products;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    MoneyDto Price,
    Guid IdStock,
    string? Description,
    List<ImageDto>? Images,
    int? Quantity,
    Guid? BucketId);