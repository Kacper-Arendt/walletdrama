using Shared.Abstractions.Exceptions;

namespace Budgets.Core.Exceptions;

public class CategoryNotFoundException(Guid id) : CustomException($"Category with id: {id} not found");