namespace EdgedGoods.Domain.Common.Shared;

public abstract class AuditableEntity<TId> : BaseEntity<TId>, IAuditable where TId : struct
{
    public bool IsAuditable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    protected AuditableEntity(TId id) : base(id) { }
    protected AuditableEntity() { }
}