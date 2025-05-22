using Shared.Abstractions.Communication;
using Shared.Abstractions.Events;
using Shared.Abstractions.ValueObjects;

namespace Teams.Core.Events;

public class InvitationCreatedEventHandler(IEmailSender emailSender) : IEventHandler<InvitationCreatedEvent>
{
    public async Task HandleAsync(InvitationCreatedEvent @event)
    {
        var body = $"""
                    <p style='color:#555;font-size:16px;font-family:sans-serif;margin-bottom:32px;'>
                        You have been invited to join the team <strong>{@event.TeamName.Value}</strong>. Click below to accept the invitation.
                    </p>
                    <a href='xx' style='display:inline-block;padding:12px 28px;background-color:#4f46e5;color:#fff;text-decoration:none;border-radius:4px;font-size:16px;font-family:sans-serif;'>
                        Accept Invitation
                    </a>
                    """;

        await emailSender.SendEmailAsync(
            [new Email(@event.Email.Value)],
            "Invitation to join team",
            body
        );
    }
}