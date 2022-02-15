using System.Reflection;
using Invoices.Domain;
using Invoices.Infrastructure.Notifications.Email;
using Invoices.Infrastructure.Persistence.MongoDb;
using Invoices.Shared.Infrastructure.Email;
using Invoices.Shared.Infrastructure.MongoDb;
using MediatR;

namespace Invoices.Api.Extensions.DependencyInjection;

public static class Infrastructure
{
    private const string InvoicesAssemblyName = "Invoices";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<InvoicesDatabaseSettings>(configuration.GetSection("InvoicesDatabase"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddMediatR(typeof(Program));
        services.AddMediatR(Assembly.Load(InvoicesAssemblyName));

        services.AddScoped<IInvoicesRepository, MongoDbInvoicesRepository>();
        services.AddScoped<IInvoiceNotifier, EmailInvoiceNotifier>();

        return services;
    }

    public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app, IConfiguration configuration)
    {
        var allowedUrls = configuration.GetSection("AllowedUrls").Get<List<string>>();
        app.UseCors(c =>
            c.WithOrigins(allowedUrls.ToArray()).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

        return app;
    }
}