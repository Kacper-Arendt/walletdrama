using Shared.Abstractions.Exceptions;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Exceptions;

public class TeamIdMismatchException()
    : CustomException("The team ID provided does not match the ID of the team being updated.");