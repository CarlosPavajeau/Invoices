using Invoices.Application.Notify;
using Invoices.Domain;
using MediatR;

namespace Invoices.Application.SearchAll;

public class SearchAllInvoicesQueryHandler : IRequestHandler<SearchAllInvoicesQuery, IEnumerable<InvoiceResponse>>
{
    private readonly InvoicesSearcher _searcher;
    private readonly InvoiceNotifier _notifier;

    public SearchAllInvoicesQueryHandler(InvoicesSearcher searcher, InvoiceNotifier notifier)
    {
        _searcher = searcher;
        _notifier = notifier;
    }

    public async Task<IEnumerable<InvoiceResponse>> Handle(SearchAllInvoicesQuery request,
        CancellationToken cancellationToken)
    {
        var invoices = await _searcher.SearchAll();

        await NotifyInvoices(invoices);

        return invoices.Select(InvoiceResponse.FromAggregate);
    }

    private async Task NotifyInvoices(IEnumerable<Invoice> invoices)
    {
        var invoicesToNotify =
            invoices.Where(i => i.State is InvoiceState.SecondReminder or InvoiceState.Disabled).ToList();
        await _notifier.Notify(invoicesToNotify);
    }
}