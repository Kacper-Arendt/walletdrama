using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Abstractions.ValueObjects;

namespace Budgets.Persistence.Converters;

public class UserIdConverter() : ValueConverter<UserId, Guid>(
    modelValue => modelValue.Value,
    dbValue => new UserId(dbValue)
);