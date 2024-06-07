using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Products.ValueObjects;

namespace EdgedGoods.Domain.Orders.Events;

public readonly record struct QuantityValueChanged(ProductId ProductId, int OldValue, int NewValue) : IDomainEvent;