using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Entities;
using Teams.Domain.ValueObjects;
using Teams.Persistence.Converters;

namespace Teams.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams", "Teams");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .IsRequired()
            .HasConversion<TeamIdConverter>();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasConversion<TeamNameConverter>();

        builder.Property(t => t.OwnerId)
            .IsRequired()
            .HasConversion<UserIdConverter>();

        builder.HasMany(t => t.Members)
            .WithOne()
            .HasForeignKey(m => m.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.OwnerId)
            .HasDatabaseName("IX_Teams_OwnerId");
    }
}