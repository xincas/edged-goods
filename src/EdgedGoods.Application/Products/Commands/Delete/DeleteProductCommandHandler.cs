using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Domain.Products.Errors;
using EdgedGoods.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.Products.Commands.Delete;

public class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IStockItemRepository stockItemRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var id = new ProductId(request.Id);

        var product = await productRepository.GetByIdAsync(id);
        
        if (product is null)
        {
            return ProductErrors.ProductDoesNotExists;
        }

        await productRepository.RemoveAsync(product);
        
        var stockItems = await stockItemRepository.ListByProductIdAsync(id);
        if (stockItems.Count > 0)
        {
            await stockItemRepository.RemoveRangeAsync(stockItems);
        }

        await unitOfWork.CommitChangesAsync();
        
        return Result.Deleted;
    }
}