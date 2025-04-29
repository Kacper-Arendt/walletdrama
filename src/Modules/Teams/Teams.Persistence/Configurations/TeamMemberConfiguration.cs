using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Entities;
using Teams.Persistence.Converters;

namespace Teams.Persistence.Configurations;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.ToTable("TeamMembers", "Teams");

        builder.HasKey(t => t.MemberId);
        
        builder.Property(t => t.MemberId)
            .IsRequired()
            .HasConversion<TeamMemberIdConverter>();

        builder.Property(t => t.TeamId)
            .IsRequired()
            .HasConversion<TeamIdConverter>();
        
        builder.Property(t => t.UserId)
            .IsRequired()
            .HasConversion<UserIdConverter>();
        
        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(255)
            .HasConversion<EmailConverter>();
        
        builder.Property(t => t.Role).IsRequired();

        builder.HasIndex(t => t.TeamId)
            .HasDatabaseName("IX_TeamMembers_TeamId");

        builder.HasIndex(t => new { t.TeamId, t.UserId })
            .IsUnique()
            .HasDatabaseName("UQ_TeamMembers_TeamId_UserId");
    }
}