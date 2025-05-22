using Shared.Abstractions.Exceptions;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Exceptions;

public class InvitationNotFoundException(Guid id) : CustomException($"Invitation with id: {id} not found");