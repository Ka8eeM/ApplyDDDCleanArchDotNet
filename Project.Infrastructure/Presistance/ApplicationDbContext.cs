using Microsoft.EntityFrameworkCore;
using Project.Domain.Common.Premitives;
using Project.Domain.MenuAggregate;
using Project.Domain.UserAggregate;
using Project.Infrastructure.Presistance.Interceptors;
using System.Reflection;

namespace Project.Infrastructure.Presistance;

public sealed class ApplicationDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        PublishDomainEventsInterceptor interceptor) : base(options)
    {
        _publishDomainEventsInterceptor = interceptor;
    }

    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}