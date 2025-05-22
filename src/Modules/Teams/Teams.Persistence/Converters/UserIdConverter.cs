using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Abstractions.ValueObjects;
using Teams.Domain.ValueObjects;

namespace Teams.Persistence.Converters;

public class UserIdConverter() : ValueConverter<UserId, Guid>(
    modelValue => modelValue.Value,
    dbValue => new UserId(dbValue)
);