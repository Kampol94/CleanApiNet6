using CleanApi.Shared.DTOs.Multitenancy;

namespace CleanApi.Application.Abstractions.Services.General
{
    public interface ITenantService : IScopedService
    {
        public string GetDatabaseProvider();
        public string GetConnectionString();
        public TenantDto GetCurrentTenant();
    }
}