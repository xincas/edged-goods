using EdgedGoods.Domain.Stocks;
using EdgedGoods.Domain.Stocks.ValueObjects;

namespace EdgedGoods.Application.Common.Interfaces;

public interface IStockRepository
{
    Task<Stock?> GetByIdAsync(StockId id);
    Task<bool> IsExistAsync(StockId id);
    Task<List<Stock>> ListAsync();
    Task AddAsync(Stock stock);
    Task UpdateAsync(Stock stock);
    Task RemoveAsync(Stock stock);
    Task RemoveRangeAsync(List<Stock> stocks);
}