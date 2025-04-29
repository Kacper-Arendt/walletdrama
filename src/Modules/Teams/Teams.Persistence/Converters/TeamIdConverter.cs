using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teams.Domain.ValueObjects;

namespace Teams.Persistence.Converters;

public class TeamIdConverter() : ValueConverter<TeamId, Guid>(
    modelValue => modelValue.Value,
    dbValue => new TeamId(dbValue)
);