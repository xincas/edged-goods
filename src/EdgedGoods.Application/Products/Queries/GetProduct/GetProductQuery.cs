using EdgedGoods.Domain.Products;
using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.Products.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IRequest<ErrorOr<Product>>;