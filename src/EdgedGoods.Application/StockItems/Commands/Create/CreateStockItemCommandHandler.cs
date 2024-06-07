using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Domain.Products.Errors;
using EdgedGoods.Domain.Products.ValueObjects;
using EdgedGoods.Domain.Stocks;
using EdgedGoods.Domain.Stocks.Errors;
using EdgedGoods.Domain.Stocks.ValueObjects;
using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.StockItems.Commands.Create;

public sealed class CreateStockItemCommandHandler(
    IProductRepository productRepository,
    IStockItemRepository stockItemRepository,
    IStockRepository stockRepository,
    IBucketRepository bucketRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateStockItemCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateStockItemCommand request, CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.ProductId);
        var product = await productRepository.GetByIdAsync(productId);

        if (product is null)
        {
            return ProductErrors.ProductDoesNotExists;
        }
        
        
        var stockId = new StockId(request.StockId);
        var stock = await stockRepository.GetByIdAsync(stockId);

        if (stock is null)
        {
            return StockErrors.StockDoesNotExists;
        }

        var stockItem = new StockItem(productId, request.Quantity, stockId);
            
        if (request.BucketId is { } id)
        {
            var bucketId = new BucketId(id);
            var bucket = await bucketRepository.GetByIdAsync(bucketId);

            if (bucket is not null)
            {
                stockItem.BucketId = bucketId;
            }
            
            bucket?.Products.Add(stockItem);
        }
        
        await stockItemRepository.AddAsync(stockItem);
        await unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}