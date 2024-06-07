using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Stocks.ValueObjects;

namespace EdgedGoods.Domain.Stocks;

public class Bucket : AuditableEntity<BucketId>
{
    public required string Code { get; set; }
    public string? Description { get; set; }
    public List<StockItem> Products { get; set; } = [];

    public StockId StockId { get; set; }
    public Stock Stock { get; set; } = null!;
}