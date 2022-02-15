using MediatR;

namespace Invoices.Application.SearchAll;

public record SearchAllInvoicesQuery : IRequest<IEnumerable<InvoiceResponse>>;