using Shared.Abstractions.Exceptions;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Exceptions;

public class TeamNotFoundException(Guid id) : CustomException($"Team with id: {id} not found");