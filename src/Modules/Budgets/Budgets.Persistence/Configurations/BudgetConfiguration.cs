using Budgets.Domain.Entities;
using Budgets.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgets.Persistence.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budget", "Budgets");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .IsRequired()
            .HasConversion<BudgetIdConverter>();

        builder.OwnsOne(x => x.Details, d =>
        {
            d.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasConversion<BudgetNameConverter>();

            d.Property(p => p.Description)
                .HasMaxLength(1000);
        });

        builder.Property(t => t.OwnerId)
            .IsRequired()
            .HasConversion<UserIdConverter>();

        builder.HasIndex(t => t.OwnerId)
            .HasDatabaseName("IX_Teams_OwnerId");
    }
}