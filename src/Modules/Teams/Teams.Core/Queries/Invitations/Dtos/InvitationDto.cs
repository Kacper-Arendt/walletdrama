using Shared.Abstractions.ValueObjects;
using Teams.Domain.Const;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Queries.Invitations.Dtos;

public record InvitationDto(Guid Id, TeamId TeamId, Email Email, DateTime CreatedAt, TeamRole Role, InvitationStatus Status);