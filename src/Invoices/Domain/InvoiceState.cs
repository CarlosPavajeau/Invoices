using System.Text.Json.Serialization;

namespace Invoices.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum InvoiceState
{
    FirstReminder,
    SecondReminder,
    Disabled
}