using Teams.Domain.Entities;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Queries.Dtos;

public record TeamDetailsDto(Guid Id, string Name, Guid OwnerId, List<TeamMemberDto> Members);