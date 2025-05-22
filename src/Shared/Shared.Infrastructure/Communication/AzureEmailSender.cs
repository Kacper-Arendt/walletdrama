using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Shared.Abstractions.Communication;
using Shared.Abstractions.ValueObjects;

namespace Shared.Infrastructure.Communication;

public class AzureEmailSender : IEmailSender
{
    private readonly EmailClient _client;
    private readonly string _fromAddress;
    
    public AzureEmailSender(IConfiguration configuration)
    {
        _client = new EmailClient(configuration["EmailService:ConnectionString"]);
        _fromAddress = configuration["EmailService:From"];
    }

    public async Task SendEmailAsync(List<Email> mails, string subject, string body)
    {
        var recipients = mails.Select(mail => new EmailAddress(mail)).ToList();

        var emailMessage = new EmailMessage(
            senderAddress: _fromAddress,
            content: new EmailContent($"Wallet Drama: {subject}")
            {
                Html = EmailTemplate.Wrap(body)
            },
            recipients: new EmailRecipients(recipients));


        await _client.SendAsync(
            WaitUntil.Completed,
            emailMessage);
    }
}