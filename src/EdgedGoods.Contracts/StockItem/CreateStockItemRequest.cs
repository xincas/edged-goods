namespace EdgedGoods.Contracts.StockItem;

public record CreateStockItemRequest(Guid ProductId, Guid StockId, int Quantity, Guid? BucketId);

public record UpdateStockItemRequest(Guid ProductId, Guid StockId, int Quantity, Guid? BucketId);