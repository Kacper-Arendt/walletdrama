using Teams.Core.Queries.Dtos;

namespace Teams.Core.Queries;

public interface ITeamsQueryService
{
    Task<TeamDetailsDto> GetAsync(Guid teamId);
    Task<IEnumerable<TeamDto>> GetListAsync();
    Task<bool> IsUserOwnerOfTeamAsync(Guid userId, Guid teamId);
}