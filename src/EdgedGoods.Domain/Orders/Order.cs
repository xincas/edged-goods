using System.Diagnostics.CodeAnalysis;
using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Common.ValueObjects;
using EdgedGoods.Domain.Orders.Errors;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.ValueObjects;
using EdgedGoods.Domain.Users;
using ErrorOr;

namespace EdgedGoods.Domain.Orders;

public class Order : AuditableEntity<OrderId>
{
    public UserId UserId { get; set; }
    public User User { get; set; } = null!;
    public string? Note { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
    public Money TotalPrice { get; set; }

    [SetsRequiredMembers]
    public Order(OrderId orderId, UserId userId, List<OrderItem> orderOrderItems, string? note = null) : base(orderId)
    {
        UserId = userId;
        OrderItems = orderOrderItems;
        Note = note;
        RecalculateTotal();
    }
    
    private void RecalculateTotal() => TotalPrice = OrderItems.Select(oi => oi.Price * oi.Quantity).Sum();

    public ErrorOr<Success> DeleteOrderItem(ProductId productId)
    {
        if (!TryGetOrderItem(productId, out var item))
        {
            return OrderErrors.ItemAlreadyDeleted;
        }
        
        OrderItems.Remove(item);
        RecalculateTotal();
        return Result.Success;
    }
    
    public ErrorOr<Created> AddOrderItem(Product product, int? quantity = null)
    {
        if (!IsItemExists(product.Id))
        {
            return OrderErrors.ItemAlreadyExists;
        }

        var index = OrderItems.Count;    
        var orderItem = new OrderItem(
            index,
            Id,
            product.Id,
            quantity ?? 1,
            product.Price);
        
        OrderItems.Add(orderItem);
        RecalculateTotal();
        return Result.Created;
    }

    public ErrorOr<Created> AddOrderItemsRange(IEnumerable<(Product product, int? quantity)> productsWithQuantity)
    {
        List<OrderItem> productsToAdd = [];
        List<Error> errors = [];
        
        foreach (var (product, quantity) in productsWithQuantity)
        {
            if (!IsItemExists(product.Id))
            {
                errors.Add(OrderErrors.ItemAlreadyExists);
                continue;
            }
            var index = OrderItems.Count;    
            var orderItem = new OrderItem(
                index,
                Id,
                product.Id,
                quantity ?? 1,
                product.Price);
        
            productsToAdd.Add(orderItem);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        OrderItems.AddRange(productsToAdd);
        RecalculateTotal();
        return Result.Created;
    }
    
    private bool IsItemExists(ProductId productId) 
        => OrderItems.FirstOrDefault(oi => oi.ProductId == productId) is not null;

    private bool TryGetOrderItem(ProductId productId, [MaybeNullWhen(false)] out OrderItem product)
    {
        product = OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
        return product is not null;
    }
}