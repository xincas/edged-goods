using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Orders;

namespace EdgedGoods.Domain.Users;

public class User : BaseEntity<UserId>
{
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public List<Order> Orders { get; set; } = [];
    public string? Email { get; set; }
    public string? LastName { get; set; }
}