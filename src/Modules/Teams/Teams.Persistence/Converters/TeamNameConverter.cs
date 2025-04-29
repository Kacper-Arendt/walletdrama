using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teams.Domain.ValueObjects;

namespace Teams.Persistence.Converters;

public class TeamNameConverter() : ValueConverter<TeamName, string>(
    modelValue => modelValue.Value,
    dbValue => new TeamName(dbValue)
);