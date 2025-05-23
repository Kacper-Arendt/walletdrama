using Budgets.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Budgets.Persistence.Converters;

public class BudgetIdConverter() : ValueConverter<BudgetId, Guid>(
    modelValue => modelValue.Value,
    dbValue => new BudgetId(dbValue)
);