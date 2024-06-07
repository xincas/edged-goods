using EdgedGoods.Application.Common.Interfaces;
using EdgedGoods.Domain.Common.Shared;
using EdgedGoods.Domain.Orders;
using EdgedGoods.Domain.Products;
using EdgedGoods.Domain.Stocks;
using EdgedGoods.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EdgedGoods.Infrastructure;

public sealed class ApplicationDbContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor,
    IPublisher publisher) : DbContext, IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Stock> Stocks { get; set; }    
    public DbSet<Bucket> Buckets { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public async Task CommitChangesAsync()
    {
        // get hold of all the domain events
        var domainEvents = ChangeTracker.Entries()
            .Where(e =>
            {
                var baseType = e.Entity.GetType().BaseType;

                return baseType is not null && (baseType == typeof(AuditableEntity<>) || baseType == typeof(BaseEntity<>));
            })
            .Select(entry => (entry.Entity as BaseEntity)?.PopDomainEvents() ?? [])
            .SelectMany(x => x)
            .ToList();

        // store them in the http context for later if user is waiting online
        if (IsUserWaitingOnline())
        {
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
        }
        else
        {
            await PublishDomainEvents(publisher, domainEvents);
        }

        await SaveChangesAsync();
    }
    
    private static async Task PublishDomainEvents(IPublisher publisher, List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }

    private bool IsUserWaitingOnline() => httpContextAccessor.HttpContext is not null;

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        // fetch queue from http context or create a new queue if it doesn't exist
        var domainEventsQueue = httpContextAccessor.HttpContext!.Items
            .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
            ? existingDomainEvents
            : new Queue<IDomainEvent>();

        // add the domain events to the end of the queue
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        // store the queue in the http context
        httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }
}