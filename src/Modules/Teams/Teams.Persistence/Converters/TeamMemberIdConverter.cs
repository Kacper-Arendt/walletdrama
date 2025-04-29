using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teams.Domain.ValueObjects;

namespace Teams.Persistence.Converters;

public class TeamMemberIdConverter() : ValueConverter<TeamMemberId, Guid>(
    modelValue => modelValue.Value,
    dbValue => new TeamMemberId(dbValue)
);