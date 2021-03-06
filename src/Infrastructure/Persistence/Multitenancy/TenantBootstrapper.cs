using CleanApi.Application.Settings;
using CleanApi.Domain.Constants;
using CleanApi.Domain.Entities.Multitenancy;
using CleanApi.Infrastructure.Identity.Models;
using CleanApi.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Npgsql;
using Serilog;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanApi.Infrastructure.Persistence.Multitenancy
{
    public class TenantBootstrapper
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(TenantBootstrapper));
        public static void Initialize(ApplicationDbContext appContext, MultitenancySettings options, Tenant tenant, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var connectionString = string.IsNullOrEmpty(tenant.ConnectionString) ? options.ConnectionString : tenant.ConnectionString;
            var isValid = TryValidateConnectionString(options, connectionString, tenant.Key);
            if (isValid)
            {
                appContext.Database.SetConnectionString(connectionString);
                if (appContext.Database.GetMigrations().Count() > 0)
                {
                    if (appContext.Database.GetPendingMigrations().Any())
                    {
                        _logger.Information($"Applying Migrations for '{tenant.Key}' tenant.");
                        appContext.Database.Migrate();
                    }

                    if (appContext.Database.CanConnect())
                    {
                        _logger.Information($"Connection to {tenant.Key}'s Database Succeeded.");
                        SeedRoles(tenant, roleManager, appContext);
                        SeedAdmin(tenant, userManager, roleManager, appContext);
                    }
                }
            }
        }

        private static void SeedRoles(Tenant tenant, RoleManager<ApplicationRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            foreach (string roleName in typeof(RoleConstants).GetAllPublicConstantValues<string>())
            {
                var roleStore = new RoleStore<ApplicationRole>(applicationDbContext);
                if (!applicationDbContext.Roles.IgnoreQueryFilters().Any(r => r.Name == roleName && r.TenantKey == tenant.Key))
                {
                    var role = new ApplicationRole(roleName, tenant.Key, $"{roleName} Role for {tenant.Key} Tenant");
                    roleStore.CreateAsync(role).Wait();
                    _logger.Information($"Seeding {roleName} Role for '{tenant.Key}' Tenant.");
                }
            }
        }

        private static void SeedAdmin(Tenant tenant, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            var adminUserName = $"{tenant.Key.Trim().ToLower()}.admin";
            var superUser = new ApplicationUser
            {
                FirstName = tenant.Key.Trim().ToLower(),
                LastName = "admin",
                Email = tenant.AdminEmail,
                UserName = adminUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = tenant.AdminEmail.ToUpper(),
                NormalizedUserName = adminUserName.ToUpper(),
                IsActive = true,
                TenantKey = tenant.Key.Trim().ToLower()
            };
            if (!applicationDbContext.Users.IgnoreQueryFilters().Any(u => u.Email == tenant.AdminEmail))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(superUser, MultitenancyConstants.DefaultPassword);
                superUser.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(applicationDbContext);
                userStore.CreateAsync(superUser).Wait();
                _logger.Information($"Seeding Default Admin User for '{tenant.Key}' Tenant.");
            }

            AssignAdminRoleAsync(superUser.Email, tenant.Key, applicationDbContext, userManager, roleManager).Wait();
        }

        public static async Task AssignAdminRoleAsync(string email, string tenantKey, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var adminRole = RoleConstants.Admin;
            var user = await userManager.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (user == null) return;
            var roleRecord = roleManager.Roles.IgnoreQueryFilters().Where(a => a.NormalizedName == adminRole.ToUpper() && a.TenantKey == tenantKey).FirstOrDefaultAsync().Result;
            if (roleRecord == null) return;
            var isUserInRole = applicationDbContext.UserRoles.Any(a => a.UserId == user.Id && a.RoleId == roleRecord.Id);
            if (!isUserInRole)
            {
                applicationDbContext.UserRoles.Add(new IdentityUserRole<string>() { RoleId = roleRecord.Id, UserId = user.Id });
                await applicationDbContext.SaveChangesAsync();
                _logger.Information($"Assigning Admin Permissions for '{tenantKey}' Tenant.");
            }

            foreach (string permission in typeof(Permissions).GetNestedClassesStaticStringValues())
            {
                var allClaims = await roleManager.GetClaimsAsync(roleRecord);
                if (!allClaims.Any(a => a.Type == Domain.Constants.ClaimConstants.Permission && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(roleRecord, new Claim(Domain.Constants.ClaimConstants.Permission, permission));
                }
            }

            if (tenantKey == MultitenancyConstants.Root.Key && email == MultitenancyConstants.Root.EmailAddress)
            {
                foreach (string rootPermission in typeof(RootPermissions).GetNestedClassesStaticStringValues())
                {
                    var allClaims = await roleManager.GetClaimsAsync(roleRecord);
                    if (!allClaims.Any(a => a.Type == Domain.Constants.ClaimConstants.Permission && a.Value == rootPermission))
                    {
                        await roleManager.AddClaimAsync(roleRecord, new Claim(Domain.Constants.ClaimConstants.Permission, rootPermission));
                    }
                }

            }

            await applicationDbContext.SaveChangesAsync();
        }

        public static bool TryValidateConnectionString(MultitenancySettings options, string connectionString, string key)
        {
            try
            {
                switch (options.DBProvider)
                {
                    case "postgresql":
                        var postgresqlcs = new NpgsqlConnectionStringBuilder(connectionString);
                        break;
                    case "mysql":
                        var mysqlcs = new MySqlConnectionStringBuilder(connectionString);
                        break;
                    case "mssql":
                        var mssqlcs = new SqlConnectionStringBuilder(connectionString);
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"{key} Connection String Exception : {ex.Message}");
                return false;
            }
        }
    }
}