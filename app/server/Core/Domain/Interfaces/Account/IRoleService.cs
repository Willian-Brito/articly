using Articly.Core.Domain.Enums;

namespace Articly.Core.Domain.Account;

public interface IRoleService
{
    List<Role> GetAllRoles();
    List<Role> Convert(List<int> roles);
    List<string> GetAllRolesNames();
    List<string> GetNamesByRoles(List<Role> roles);
}
