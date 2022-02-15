using MediatR;

namespace Invoices.Application.SearchAll;

public class SearchAllInvoicesQueryHandler : IRequestHandler<SearchAllInvoicesQuery, IEnumerable<InvoiceResponse>>
{
    private readonly InvoicesSearcher _searcher;

    public SearchAllInvoicesQueryHandler(InvoicesSearcher searcher)
    {
        _searcher = searcher;
    }

    public async Task<IEnumerable<InvoiceResponse>> Handle(SearchAllInvoicesQuery request,
        CancellationToken cancellationToken)
    {
        var invoices = await _searcher.SearchAll();

        return invoices.Select(InvoiceResponse.FromAggregate);
    }
}