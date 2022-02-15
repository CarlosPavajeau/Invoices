using Invoices.Domain;

namespace Invoices.Application;

public record InvoiceResponse(
    string? Id,
    string Customer,
    string City,
    string Nit,
    decimal Total,
    decimal Subtotal,
    decimal Iva,
    decimal Retention,
    DateTime CreatedAt,
    string State,
    bool Paid,
    DateTime? PaidAt)
{
    public static InvoiceResponse FromAggregate(Invoice invoice)
    {
        var state = invoice.State switch
        {
            InvoiceState.FirstReminder => "Primer recordatorio",
            InvoiceState.SecondReminder => "Segundo recordatorio",
            InvoiceState.Disabled => "Servicio desactivado",
            _ => "Desconocido"
        };

        return new InvoiceResponse(
            invoice.Id,
            invoice.Customer,
            invoice.City,
            invoice.Nit,
            invoice.Total,
            invoice.Subtotal,
            invoice.Iva,
            invoice.Retention,
            invoice.CreatedAt,
            state,
            invoice.Paid,
            invoice.PaidAt);
    }
}