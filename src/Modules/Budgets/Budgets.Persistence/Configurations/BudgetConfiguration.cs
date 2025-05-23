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

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasConversion<BudgetNameConverter>();

        builder.HasIndex(b => b.Name)
            .HasDatabaseName("IX_Budgets_Name");
    }
}