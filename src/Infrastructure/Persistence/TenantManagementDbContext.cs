using CleanApi.Domain.Entities.Multitenancy;
using Microsoft.EntityFrameworkCore;

namespace CleanApi.Infrastructure.Persistence
{
    public class TenantManagementDbContext : DbContext
    {
        public TenantManagementDbContext(DbContextOptions<TenantManagementDbContext> options)
        : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}