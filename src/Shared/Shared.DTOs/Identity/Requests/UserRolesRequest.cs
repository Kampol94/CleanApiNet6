using System.Collections.Generic;

namespace CleanApi.Shared.DTOs.Identity.Requests
{
    public class UserRolesRequest
    {
        public List<UserRoleDto> UserRoles { get; set; } = new();
    }
}