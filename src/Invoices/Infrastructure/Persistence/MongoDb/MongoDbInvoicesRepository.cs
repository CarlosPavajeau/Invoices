using Invoices.Domain;
using Invoices.Shared.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Invoices.Infrastructure.Persistence.MongoDb;

public class MongoDbInvoicesRepository : IInvoicesRepository
{
    private readonly IMongoCollection<Invoice> _invoices;

    public MongoDbInvoicesRepository(IOptions<InvoicesDatabaseSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.DatabaseName);

        _invoices = database.GetCollection<Invoice>(options.Value.CollectionName);
    }

    public async Task<IEnumerable<Invoice>> SearchAll() => await _invoices.Find(invoice => true).ToListAsync();

    public async Task<Invoice?> UpdateState(string id, string state)
    {
        var invoice = await _invoices.Find(i => i.Id == id).FirstOrDefaultAsync();
        if (invoice is null) return null;

        invoice.State = state;
        await _invoices.ReplaceOneAsync(i => i.Id == id, invoice);

        return invoice;
    }
}