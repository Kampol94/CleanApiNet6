using System.Collections.Generic;

namespace CleanApi.Shared.DTOs.Identity.Responses
{
    public class UserRolesResponse
    {
        public List<UserRoleDto> UserRoles { get; set; } = new();
    }
}