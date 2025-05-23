using Budgets.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Budgets.Persistence.Converters;

public class BudgetNameConverter() : ValueConverter<BudgetName, string>(
    modelValue => modelValue.Value,
    dbValue => new BudgetName(dbValue)
);