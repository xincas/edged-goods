using ErrorOr;

namespace EdgedGoods.Domain.Orders.Errors;

public static class OrderErrors
{
    public static Error ItemAlreadyDeleted => Error.Validation(
        code: $"{nameof(OrderErrors)}.{nameof(ItemAlreadyDeleted)}",
        description: "Item that you trying to delete is already deleted.");
    
    public static Error ItemAlreadyExists => Error.Validation(
        code: $"{nameof(OrderErrors)}.{nameof(ItemAlreadyExists)}",
        description: "Item that you trying to add to order is already exists.");
}