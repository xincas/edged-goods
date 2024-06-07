using ErrorOr;

namespace EdgedGoods.Domain.Products.Errors;

public static class ProductErrors
{
    public static Error ImageDoesNotExists => Error.Validation(
        $"{nameof(ProductErrors)}.{nameof(ImageDoesNotExists)}",
        "Image that you trying to delete doesn't exists");

    public static Error ImageAlreadyExists => Error.Validation(
        $"{nameof(ProductErrors)}.{nameof(ImageAlreadyExists)}",
        "Image that you trying to add already exists");
    
    public static Error ProductDoesNotExists => Error.NotFound(
        $"{nameof(ProductErrors)}.{nameof(ProductDoesNotExists)}",
        "Product does not exists");
}