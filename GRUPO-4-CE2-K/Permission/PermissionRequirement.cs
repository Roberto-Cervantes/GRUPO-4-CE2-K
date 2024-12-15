using Microsoft.AspNetCore.Authorization;


namespace GRUPO_4_CE2_K.Permission
{
    internal class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
}
