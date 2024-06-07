using EdgedGoods.Domain.Products.ValueObjects;
using EdgedGoods.Domain.Stocks;
using EdgedGoods.Domain.Stocks.ValueObjects;

namespace EdgedGoods.Application.Common.Interfaces;

public interface IStockItemRepository
{
    Task AddAsync(StockItem product);
    Task<StockItem?> GetByProductIdAsync(ProductId id);
    Task<bool> IsAvailableAsync(ProductId id);
    Task<List<StockItem>> ListAsync();
    Task<List<StockItem>> ListByProductIdAsync(ProductId id);
    Task<List<StockItem>> ListByStockIdAsync(StockId id);
    Task<List<StockItem>> ListByBucketIdAsync(BucketId id);
    Task UpdateAsync(StockItem product);
    Task RemoveAsync(StockItem product);
    Task RemoveRangeAsync(List<StockItem> products);
}