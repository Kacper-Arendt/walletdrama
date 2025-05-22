using Teams.Domain.ValueObjects;

namespace Teams.Core.Queries.Dtos;

public record TeamDto(Guid Id, string Name, Guid OwnerId);