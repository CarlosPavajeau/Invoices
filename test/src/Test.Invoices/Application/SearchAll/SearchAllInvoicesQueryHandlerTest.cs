using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Invoices.Application.Notify;
using Invoices.Application.SearchAll;
using Invoices.Domain;
using Moq;
using Test.Invoices.Domain;
using Xunit;

namespace Test.Invoices.Application.SearchAll;

public class SearchAllInvoicesQueryHandlerTest : UnitTestCase
{
    private readonly SearchAllInvoicesQueryHandler _handler;

    public SearchAllInvoicesQueryHandlerTest()
    {
        _handler = new SearchAllInvoicesQueryHandler(new InvoicesSearcher(Repository.Object),
            new InvoiceNotifier(new Mock<IInvoiceNotifier>().Object));

        Repository.Setup(x => x.SearchAll()).ReturnsAsync(new List<Invoice>
        {
            InvoiceMother.Random(),
            InvoiceMother.Random(),
            InvoiceMother.Random()
        });
    }

    [Fact]
    public async Task Handle_ShouldReturnAllInvoices()
    {
        // Arrange
        var query = new SearchAllInvoicesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .HaveCount(3);
    }
}