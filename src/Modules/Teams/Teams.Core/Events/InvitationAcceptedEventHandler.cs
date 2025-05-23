using Shared.Abstractions.Events;

namespace Teams.Core.Events;

public class InvitationAcceptedEventHandler() : IEventHandler<InvitationAcceptedEvent>
{
    public async Task HandleAsync(InvitationAcceptedEvent @event)
    {
        Console.WriteLine($"Invitation accepted by {@event.Email} for team {@event.TeamId} with role {@event.Role}.");
    }
}