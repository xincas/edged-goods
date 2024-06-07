using System.Diagnostics.CodeAnalysis;
using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.ValueObjects;
using EdgedGoods.Domain.Stocks.ValueObjects;

namespace EdgedGoods.Domain.Stocks;

public class StockItem : AuditableEntity<ProductId>
{
    public required ProductId ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; }
    
    public required StockId StockId { get; set; }
    public required Stock Stock { get; set; }
    
    public Bucket? Bucket { get; set; }
    public BucketId? BucketId { get; set; }

    [SetsRequiredMembers]
    public StockItem(ProductId productId, int quantity, StockId stockId, BucketId? bucketId = null)
    {
        ProductId = productId;
        Quantity = quantity;
        StockId = stockId;
        BucketId = bucketId;
    }
}