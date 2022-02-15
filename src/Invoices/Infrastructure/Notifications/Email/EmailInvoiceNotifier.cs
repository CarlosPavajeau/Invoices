using System.Net;
using System.Net.Mail;
using Invoices.Domain;
using Invoices.Shared.Infrastructure.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Invoices.Infrastructure.Notifications.Email;

public class EmailInvoiceNotifier : IInvoiceNotifier
{
    private readonly SmtpClient _client;
    private readonly ILogger<EmailInvoiceNotifier> _logger;
    private readonly string _from;

    public EmailInvoiceNotifier(IOptions<EmailSettings> options, ILogger<EmailInvoiceNotifier> logger)
    {
        var settings = options.Value;
        _from = settings.Username;
        _logger = logger;

        _client = new SmtpClient
        {
            Host = settings.Host,
            Port = settings.Port,
            EnableSsl = true,
            Credentials = new NetworkCredential(settings.Username, settings.Password)
        };
    }

    private static string GenerateInvoiceMessage(Invoice invoice)
    {
        return invoice.State switch
        {
            InvoiceState.FirstReminder =>
                $"Estimado cliente {invoice.Customer}, le informamos que el estado de su factura a pasado a segundo recordatorio.",
            InvoiceState.SecondReminder =>
                $"Estimado cliente {invoice.Customer}, le informamos que el servicio va a ser desactivado.",
            _ => throw new ArgumentOutOfRangeException(nameof(invoice), invoice.State, null)
        };
    }

    public async Task Notify(Invoice invoice)
    {
        var message = new MailMessage(_from, invoice.CustomerEmail)
        {
            Subject = "Estado de factura",
            Body = GenerateInvoiceMessage(invoice),
            IsBodyHtml = false,
            Priority = MailPriority.High
        };

        await SendEmail(message);
    }

    private async Task SendEmail(MailMessage message)
    {
        try
        {
            await _client.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email");
        }
    }
}