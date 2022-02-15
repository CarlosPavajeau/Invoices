using Invoices.Domain;
using Moq;

namespace Test.Invoices;

public class UnitTestCase
{
    protected readonly Mock<IInvoicesRepository> Repository;

    protected UnitTestCase()
    {
        Repository = new Mock<IInvoicesRepository>();
    }
}