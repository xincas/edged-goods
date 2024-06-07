using EdgedGoods.Domain.Stocks;
using EdgedGoods.Domain.Stocks.ValueObjects;

namespace EdgedGoods.Application.Common.Interfaces;

public interface IBucketRepository
{
    Task AddAsync(Bucket bucket);
    Task<Bucket?> GetByIdAsync(BucketId id);
    Task<bool> IsExistAsync(BucketId id);
    Task<List<Bucket>> ListByStockIdAsync(StockId id);
    Task UpdateAsync(Bucket bucket);
    Task RemoveAsync(Bucket bucket);
    Task RemoveRangeAsync(List<Bucket> buckets);
}