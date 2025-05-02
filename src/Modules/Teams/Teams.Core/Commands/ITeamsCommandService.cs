using Teams.Core.Commands.Dtos;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Commands;

public interface ITeamsCommandService
{
    Task<TeamId> CreateTeamAsync(CreateTeamDto createTeamDto);
    Task DeleteTeamAsync(Guid teamId);
    Task UpdateTeamDetailsAsync(Guid id, UpdateTeamDetailsDto updateTeamDetailsDto);
}