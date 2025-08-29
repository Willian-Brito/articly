using Articly.Core.Domain.Account;
using Articly.Core.Domain.Enums;

namespace Articly.Infrastructure.Security.Auth;

public class RoleService : IRoleService
{
    public List<Role> GetAllRoles()
    {
        var allRoles = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
        return allRoles;
    }   

    public List<string> GetAllRolesNames()
    {
        var allRoleNames = Enum.GetNames(typeof(Role)).ToList();
        return allRoleNames;
    }

    public List<string> GetNamesByRoles(List<Role> roles)
    {
        return roles.Select(role => role.ToString()).ToList();
    }

    public List<Role> Convert(List<int> roles)
    {
        var allRoles = GetAllRoles();
        var newRoles = new List<Role>();

        allRoles.ForEach(role => 
        {
            var hasRole = roles.Any(r => r == (int)role);

            if(hasRole)
                newRoles.Add(role);
        });

        return newRoles;
    }  
}
