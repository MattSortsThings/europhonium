using Europhonium.Shared.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace Europhonium.Shared.Infrastructure.DataAccess.EFCore;

/// <summary>
///     Database context for the web application.
/// </summary>
public sealed class WebAppDbContext : DbContext
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebAppDbContext).Assembly);
    }
}
