using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.ValueObjects;

namespace EdgedGoods.Application.Common.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(ProductId id);
    Task<bool> IsExistAsync(ProductId id);
    Task<List<Product>> ListVisibleAsync();
    Task UpdateAsync(Product product);
    Task RemoveAsync(Product product);
    Task RemoveRangeAsync(List<Product> products);
}