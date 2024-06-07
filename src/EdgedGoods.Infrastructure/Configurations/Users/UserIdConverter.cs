using EdgedGoods.Domain.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EdgedGoods.Infrastructure.Configurations.Users;

public class UserIdConverter(ConverterMappingHints? mappingHints = null)
    : ValueConverter<UserId, string>(
        (id) => id.Value.ToString(),
        (id) => new UserId(Guid.Parse(id)),
        mappingHints
    );