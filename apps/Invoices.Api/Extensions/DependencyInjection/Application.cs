using Invoices.Application.SearchAll;

namespace Invoices.Api.Extensions.DependencyInjection;

public static class Application
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<InvoicesSearcher, InvoicesSearcher>();

        return services;
    }
}