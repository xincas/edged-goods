using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Stocks.ValueObjects;

namespace EdgedGoods.Domain.Stocks;

public class Stock : AuditableEntity<StockId>
{
    public required string Name { get; set; }
    public List<Bucket> Buckets { get; set; } = [];
    public List<StockItem> Items { get; set; } = [];
}