using Microsoft.EntityFrameworkCore;
using Teams.Domain.Entities;

namespace Teams.Persistence;

public class TeamsDbContext(DbContextOptions<TeamsDbContext> options) : DbContext(options)
{
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TeamInvitation> TeamInvitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema("Teams");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamsDbContext).Assembly);
    }
}