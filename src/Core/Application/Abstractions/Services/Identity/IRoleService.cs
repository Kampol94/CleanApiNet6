using CleanApi.Application.Wrapper;
using CleanApi.Shared.DTOs.Identity;
using CleanApi.Shared.DTOs.Identity.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApi.Application.Abstractions.Services.Identity
{
    public interface IRoleService : ITransientService
    {
        Task<Result<List<RoleDto>>> GetListAsync();

        Task<Result<List<PermissionDto>>> GetPermissionsAsync(string id);

        Task<int> GetCountAsync();

        Task<Result<RoleDto>> GetByIdAsync(string id);

        Task<Result<string>> RegisterRoleAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<List<RoleDto>>> GetUserRolesAsync(string userId);
    }
}