using Shared.Infrastructure.Helpers;
using Teams.Core.Commands.Dtos;
using Teams.Core.Exceptions;
using Teams.Domain.Entities;
using Teams.Domain.ValueObjects;
using Teams.Persistence;

namespace Teams.Core.Commands;

public class TeamsCommandService : ITeamsCommandService
{
    private readonly TeamsDbContext _dbContext;
    private readonly HttpContextHelper _httpContextHelper;
    
    public TeamsCommandService(TeamsDbContext dbContext, HttpContextHelper httpContextHelper)
    {
        _dbContext = dbContext;
        _httpContextHelper = httpContextHelper;
    }

    public async Task<TeamId> CreateTeamAsync(CreateTeamDto createTeamDto)
    {
        var user = _httpContextHelper.GetCurrentUser();
        var team = TeamFactory.Create(new TeamName(createTeamDto.Name), user.Id, user.Email);

        await _dbContext.Teams.AddAsync(team);
        await _dbContext.SaveChangesAsync();

        return team.Id;
    }

    public async Task DeleteTeamAsync(Guid teamId)
    {
        var teamIdValue = new TeamId(teamId);
        var team = await _dbContext.Teams.FindAsync(teamIdValue);

        if (team == null) throw new TeamNotFoundException(teamIdValue);

        _dbContext.Teams.Remove(team);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTeamDetailsAsync(Guid id, UpdateTeamDetailsDto updateTeamDetailsDto)
    {
        var teamIdValue = new TeamId(id);
        var updateTeamIdValue = new TeamId(updateTeamDetailsDto.Id);

        if (!teamIdValue.Equals(updateTeamIdValue))
        {
            throw new TeamIdMismatchException();
        }

        var team = await _dbContext.Teams.FindAsync(teamIdValue);

        if (team == null) throw new TeamNotFoundException(teamIdValue);

        var newTeamName = new TeamName(updateTeamDetailsDto.Name);

        if (team.Name.Equals(newTeamName)) return;

        team.Name = newTeamName;

        _dbContext.Teams.Update(team);
        await _dbContext.SaveChangesAsync();
    }
}