using Microsoft.AspNetCore.Authorization;

namespace CleanApi.Infrastructure.Identity.Permissions
{
    public class MustHavePermission : AuthorizeAttribute
    {
        public MustHavePermission(string permission)
        {
            Policy = permission;
        }
    }
}