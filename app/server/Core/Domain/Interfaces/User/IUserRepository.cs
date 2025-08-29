using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Enums;
using Articly.Core.Domain.Model;

namespace Articly.Core.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User, Users>
{
    Task<User> GetByEmail(string email);
    Task<List<Role>> GetRoles(long userID);
    Task<bool> IsAdmin(int userID);
}