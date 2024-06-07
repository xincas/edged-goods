using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Domain.Products.Errors;
using EdgedGoods.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.Products.Commands.Update;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IStockItemRepository stockItemRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, ErrorOr<ProductId>>
{
    public async Task<ErrorOr<ProductId>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var id = new ProductId(request.Id);
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ProductErrors.ProductDoesNotExists;
        }

        var stockItem = await stockItemRepository.GetByProductIdAsync(id);
        
        if (stockItem is null)
        {
            return ProductErrors.ProductDoesNotExists;
        }
        
        // Todo make product update method
        
        await unitOfWork.CommitChangesAsync();
        
        return id;
    }
}