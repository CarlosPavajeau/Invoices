using Invoices.Domain;

namespace Invoices.Application.SearchAll;

public class InvoicesSearcher
{
    private readonly IInvoicesRepository _repository;

    public InvoicesSearcher(IInvoicesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Invoice>> SearchAll()
    {
        return await _repository.SearchAll();
    }
}