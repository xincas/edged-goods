using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.Errors;
using EdgedGoods.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductQuery, ErrorOr<Product>>
{
    public async Task<ErrorOr<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var id = new ProductId(request.Id);

        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ProductErrors.ProductDoesNotExists;
        }

        return product;
    }
}