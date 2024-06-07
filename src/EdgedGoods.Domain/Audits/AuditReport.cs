using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Users;

namespace EdgedGoods.Domain.Audits;

public class AuditReport : BaseEntity<long>
{
    public required string Entity { get; set; }
    public required string Action { get; set; }
    public UserId CausedBy { get; set; }
    public DateTime Timestamp { get; set; }
}