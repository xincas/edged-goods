using System.Diagnostics.CodeAnalysis;
using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Common.ValueObjects;
using EdgedGoods.Domain.Orders.Events;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.ValueObjects;

namespace EdgedGoods.Domain.Orders;

public class OrderItem : BaseEntity<long>
{
    public OrderId OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public ProductId ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; private set; }
    public Money Price { get; set; }
    
    public Money TotalPrice => Quantity * Price;

    [SetsRequiredMembers]
    public OrderItem(long sortingId, OrderId orderId, ProductId productId, int quantity, Money price) : base(sortingId)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public void IncreaseQuantityBy(int amount)
    {
        var oldValue = Quantity;
        Quantity += amount;
        
        _domainEvents.Add(new QuantityValueChanged(ProductId, oldValue, Quantity));
    }

    public void IncrementQuantity() => _domainEvents.Add(
        new QuantityValueChanged(ProductId, Quantity++, Quantity));


    public void DecreaseQuantityBy(int amount)
    {
        var oldValue = Quantity;
        Quantity = Math.Clamp(Quantity - amount, 0, Quantity);
        
        _domainEvents.Add(new QuantityValueChanged(ProductId, oldValue, Quantity));
    }

    public void DecrementQuantity()
    {
        var oldValue = Quantity;
        Quantity = Math.Clamp(Quantity - 1, 0, Quantity);
        
        _domainEvents.Add(new QuantityValueChanged(ProductId, oldValue, Quantity));
    }
}