using Invoices.Domain;

namespace Invoices.Application.Update;

public class InvoiceUpdater
{
    private readonly IInvoicesRepository _repository;

    public InvoiceUpdater(IInvoicesRepository repository)
    {
        _repository = repository;
    }

    public async Task Update(Invoice newInvoice)
    {
        await _repository.Update(newInvoice.Id!, newInvoice);
    }
}