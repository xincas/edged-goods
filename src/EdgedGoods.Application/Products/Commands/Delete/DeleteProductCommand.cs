using ErrorOr;
using MediatR;

namespace EdgedGoods.Application.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;