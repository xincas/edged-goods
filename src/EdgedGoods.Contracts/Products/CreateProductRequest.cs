using EdgedGoods.Contracts.Common;

namespace EdgedGoods.Contracts.Products;

public record CreateProductRequest(
    string Name,
    MoneyDto Price,
    Guid IdStock,
    string? Description,
    List<ImageDto>? Images,
    int? Quantity,
    Guid? BucketId);