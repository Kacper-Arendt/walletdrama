using Budgets.Domain.Entities;
using Budgets.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Budgets.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category", "Budgets");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .IsRequired()
            .HasConversion<CategoryIdConverter>();

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion<CategoryNameConverter>();

        builder.Property(b => b.Description)
            .HasMaxLength(255);

        builder.Property(b => b.BudgetId)
            .IsRequired()
            .HasConversion<BudgetIdConverter>();

        builder.Property(b => b.IsActive)
            .IsRequired();

        builder.Property(b => b.Type)
            .IsRequired();

        builder.HasIndex(t => t.BudgetId)
            .HasDatabaseName("IX_Categories_BudgetId");

        builder.HasIndex(t => t.Type)
            .HasDatabaseName("IX_Categories_Type");

        builder.HasIndex(t => t.IsActive)
            .HasDatabaseName("IX_Categories_IsActive");
    }
}