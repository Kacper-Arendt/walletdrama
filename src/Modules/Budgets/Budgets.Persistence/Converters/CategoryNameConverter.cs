using Budgets.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Budgets.Persistence.Converters;

public class CategoryNameConverter() : ValueConverter<CategoryName, string>(
    modelValue => modelValue.Value,
    dbValue => new CategoryName(dbValue)
);