namespace Invoices.Domain;

public interface IInvoicesRepository
{
    public Task<IEnumerable<Invoice>> SearchAll();
    public Task<Invoice?> UpdateState(string id, string state);
}