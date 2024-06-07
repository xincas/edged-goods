namespace EdgedGoods.Domain.Common.Shared;

public abstract class BaseEntity<TId>: BaseEntity where TId : struct
{
    public required TId Id { get; init; }
    protected BaseEntity(TId id) => Id = id;
    protected BaseEntity() {}
}

public abstract class BaseEntity
{
    protected readonly List<IDomainEvent> _domainEvents = [];
    
    protected BaseEntity() {}

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        
        _domainEvents.Clear();

        return copy;
    }
}