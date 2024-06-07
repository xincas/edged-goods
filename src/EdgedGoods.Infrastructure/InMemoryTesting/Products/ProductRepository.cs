using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.ValueObjects;

namespace EdgedGoods.Infrastructure.InMemoryTesting.Products;

public class ProductRepository : IProductRepository
{
    private List<Product> _products = [];
    
    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task<Product?> GetByIdAsync(ProductId id) => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task<bool> IsExistAsync(ProductId id) => Task.FromResult(_products.FirstOrDefault(p => p.Id == id) is not null);

    public Task<List<Product>> ListVisibleAsync() => Task.FromResult(_products.Where(p => !p.IsHidden).ToList());

    public Task UpdateAsync(Product product)
    {
        var productInDb = _products.FirstOrDefault(p => p.Id == product.Id);
        if (productInDb is null)
        {
            return Task.CompletedTask;
        }

        _products.Remove(productInDb);
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Product product)
    {
        _products.Remove(product);
        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(List<Product> products)
    {
        products.ForEach(p => _products.Remove(p));
        return Task.CompletedTask;
    }
}