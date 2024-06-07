using ErrorOr;

namespace EdgedGoods.Domain.Stocks.Errors;

public static class StockErrors
{
    // TODO should i duplicate product error and create new for stock item or use just product error
    //public static Error ProductDoesNotExists => Error.Validation(nameof(ProductDoesNotExists), "Product does not");
    public static Error StockDoesNotExists => Error.Validation(
        $"{nameof(StockErrors)}.{nameof(StockDoesNotExists)}", 
        "Stock does not exists");
    
    public static Error BucketDoesNotExists => Error.Validation(
        $"{nameof(StockErrors)}.{nameof(BucketDoesNotExists)}", 
        "Bucket does not exists");
    
    public static Error StockItemDoesNotExists => Error.Validation(
        $"{nameof(StockErrors)}.{nameof(StockItemDoesNotExists)}", 
        "Stock item does not exists");
}