using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Model;

namespace Articly.Core.Domain.Account;

public interface IAuthenticateService
{
    Task<User> Authenticate(string email, string password);
    Task<Users> Register(string name, string password, string confirmPassword, string email);
    Task Logout();
}
