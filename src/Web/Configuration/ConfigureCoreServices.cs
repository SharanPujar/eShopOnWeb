using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Data.Queries;
using Microsoft.eShopWeb.Infrastructure.Logging;
using Microsoft.eShopWeb.Infrastructure.Services;

namespace Microsoft.eShopWeb.Web.Configuration;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        _ = services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        _ = services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        _ = services.AddScoped<IBasketService, BasketService>();
        _ = services.AddScoped<IOrderService, OrderService>();
        _ = services.AddScoped<IBasketQueryService, BasketQueryService>();

        var catalogSettings = configuration.Get<CatalogSettings>() ?? new CatalogSettings();
        _ = services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));

        _ = services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        _ = services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}
