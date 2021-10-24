using CleanApi.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace CleanApi.Infrastructure.Identity.Models
{
    public class ApplicationRole : IdentityRole, IIdentityTenant
    {
        public string Description { get; set; }
        public string TenantKey { get; set; }

        public ApplicationRole()
        : base()
        {
        }

        public ApplicationRole(string roleName, string tenantKey, string description = null)
        : base(roleName)
        {
            Description = description;
            NormalizedName = roleName.ToUpper();
            TenantKey = tenantKey;
        }
    }
}