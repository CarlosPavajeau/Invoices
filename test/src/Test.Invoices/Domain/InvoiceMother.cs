using System;
using Bogus;
using Invoices.Domain;

namespace Test.Invoices.Domain;

public static class InvoiceMother
{
    public static Invoice Random() => new()
    {
        Id = Guid.NewGuid().ToString(),
        Customer = RandomWord(),
        City = RandomWord(),
        CreatedAt = DateTime.UtcNow,
        Total = 1000,
        Subtotal = 1000,
        Iva = 200,
        Nit = RandomWord(),
        Paid = false
    };

    public static string RandomWord() => new Faker().Random.Word();
}