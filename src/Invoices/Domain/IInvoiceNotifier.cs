namespace Invoices.Domain;

public interface IInvoiceNotifier
{
    Task Notify(Invoice invoice);
}