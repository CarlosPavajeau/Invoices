namespace Invoices.Domain;

public interface IInvoicesRepository
{
    public Task<IEnumerable<Invoice>> SearchAll();
    public Task<Invoice?> UpdateState(string id, InvoiceState state);
    public Task Update(string id, Invoice newInvoice);
}