using Invoices.Domain;

namespace Invoices.Application.Notify;

public class InvoiceNotifier
{
    private readonly IInvoiceNotifier _notifier;

    public InvoiceNotifier(IInvoiceNotifier notifier)
    {
        _notifier = notifier;
    }

    public async Task Notify(IEnumerable<Invoice> invoices)
    {
        foreach (var invoice in invoices)
        {
            await _notifier.Notify(invoice);
        }
    }
}