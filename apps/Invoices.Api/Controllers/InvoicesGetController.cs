using Invoices.Application;
using Invoices.Application.SearchAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoicesGetController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoicesGetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceResponse>>> GetInvoices()
    {
        var invoices = await _mediator.Send(new SearchAllInvoicesQuery());
        return Ok(invoices);
    }
}