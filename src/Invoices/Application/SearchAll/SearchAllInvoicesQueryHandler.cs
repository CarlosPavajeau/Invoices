using Invoices.Application.Notify;
using Invoices.Application.Update;
using Invoices.Domain;
using MediatR;

namespace Invoices.Application.SearchAll;

public class SearchAllInvoicesQueryHandler : IRequestHandler<SearchAllInvoicesQuery, IEnumerable<InvoiceResponse>>
{
    private readonly InvoicesSearcher _searcher;
    private readonly InvoiceNotifier _notifier;
    private readonly InvoiceUpdater _updater;

    public SearchAllInvoicesQueryHandler(InvoicesSearcher searcher, InvoiceNotifier notifier, InvoiceUpdater updater)
    {
        _searcher = searcher;
        _notifier = notifier;
        _updater = updater;
    }

    public async Task<IEnumerable<InvoiceResponse>> Handle(SearchAllInvoicesQuery request,
        CancellationToken cancellationToken)
    {
        var invoices = (await _searcher.SearchAll()).ToList();

        await NotifyInvoices(invoices);
        await UpdateInvoices(invoices);

        return invoices.Select(InvoiceResponse.FromAggregate);
    }

    private async Task NotifyInvoices(IEnumerable<Invoice> invoices)
    {
        var invoicesToNotify =
            invoices.Where(i => i.State is InvoiceState.FirstReminder or InvoiceState.SecondReminder);
        await _notifier.Notify(invoicesToNotify);
    }

    private async Task UpdateInvoices(IEnumerable<Invoice> invoices)
    {
        foreach (var invoice in invoices)
        {
            invoice.UpdateState();
            await _updater.Update(invoice);
        }
    }
}