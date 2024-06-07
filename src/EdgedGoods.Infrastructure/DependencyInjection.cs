using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Infrastructure.InMemoryTesting.Products;
using EdgedGoods.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EdgedGoods.Infrastructure;

public static class DependencyInjection
{
    private const string PostgresConnection = "Postgres"; 
    private const string InMemoryDB = "InMemoryDB"; 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        bool isInMemory;
        var isParsed = bool.TryParse(configuration.GetSection(InMemoryDB).Value, out isInMemory);
        if (isParsed && isInMemory)
        {
            services.AddSingleton<IProductRepository, ProductRepository>();
        }
        else
        {
            services.AddSingleton<AuditInterceptor>();
            services.AddDbContext<ApplicationDbContext>((sp, optionsBuilder) =>
            {
                var auditableInterceptor = sp.GetService<AuditInterceptor>()!;
                var connectionsString = configuration.GetConnectionString(PostgresConnection);

                optionsBuilder.UseNpgsql(connectionsString)
                    .AddInterceptors(auditableInterceptor);
            });

            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<ApplicationDbContext>());
        }
        
        return services;
    }
}