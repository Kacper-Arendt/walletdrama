using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Entities;
using Teams.Persistence.Converters;

namespace Teams.Persistence.Configurations;

public class TeamInvitationConfiguration : IEntityTypeConfiguration<TeamInvitation>
{
    public void Configure(EntityTypeBuilder<TeamInvitation> builder)
    {
        builder.ToTable("TeamInvitations", "Teams");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.TeamId)
            .IsRequired()
            .HasConversion<TeamIdConverter>();

        builder.Property(t => t.Email)
            .IsRequired()
            .HasConversion<EmailConverter>();

        builder.Property(t => t.Role).IsRequired();

        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.Status).IsRequired();

        builder.HasIndex(t => t.Email)
            .HasDatabaseName("IX_TeamInvitations_Email");

        builder.HasIndex(t => t.TeamId)
            .HasDatabaseName("IX_TeamInvitations_TeamId");

        builder.HasIndex(t => new { t.TeamId, t.Email })
            .IsUnique()
            .HasDatabaseName("UQ_TeamInvitations_TeamId_UserId");
    }
}