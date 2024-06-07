namespace EdgedGoods.Domain.Common.Shared;

public interface IAuditable
{
    public bool IsAuditable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}