using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;
using Teams.Core.Authorization;
using Teams.Core.Commands;
using Teams.Core.Commands.Invitations;
using Teams.Core.Events;
using Teams.Core.Queries;
using Teams.Core.Queries.Invitations;
using Teams.Persistence;

namespace Teams.Core;

public static class Extensions
{
    public static IServiceCollection AddTeamsCore(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("IsTeamOwner", policy => { policy.Requirements.Add(new IsTeamOwnerRequirement()); })
            .AddPolicy("IsInvitationRecipient",
                policy => { policy.Requirements.Add(new IsInvitationRecipientRequirement()); });

        services.AddTeamsPersistence();

        services.AddScoped<ITeamsCommandService, TeamsCommandService>();
        services.AddScoped<ITeamsQueryService, TeamsQueryService>();

        services.AddScoped<IInvitationCommandService, InvitationCommandService>();
        services.AddScoped<IInvitationQueryService, InvitationQueryService>();

        services.AddScoped<IAuthorizationHandler, IsTeamOwnerHandler>();
        services.AddScoped<IAuthorizationHandler, IsInvitationRecipientHandler>();
        
        services.AddScoped<IEventHandler<InvitationCreatedEvent>, InvitationCreatedEventHandler>();
        services.AddScoped<IEventHandler<InvitationAcceptedEvent>, InvitationAcceptedEventHandler>();

        return services;
    }
}