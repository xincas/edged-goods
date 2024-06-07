using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.Products.Commands.Create;

public class CreateProductCommandHandler(
    IProductRepository productRepository, 
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, ErrorOr<ProductId>>
{
    public async Task<ErrorOr<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var id = new ProductId(Guid.NewGuid());
        var product = new Product(
            id: id,
            name: request.Name,
            price: request.Price,
            description: request.Description,
            images: request.Images);

        await productRepository.AddAsync(product);
        await unitOfWork.CommitChangesAsync();

        return id;
    }
}