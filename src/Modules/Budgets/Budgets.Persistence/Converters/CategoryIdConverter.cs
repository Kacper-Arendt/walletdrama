using Budgets.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Budgets.Persistence.Converters;

public class CategoryIdConverter() : ValueConverter<CategoryId, Guid>(
    modelValue => modelValue.Value,
    dbValue => new CategoryId(dbValue)
);