using Shared.Abstractions.Exceptions;

namespace Budgets.Core.Exceptions;

public class BudgetNotFoundException(Guid id) : CustomException($"Budget with id: {id} not found");