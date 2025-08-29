using Articly.Core.Domain.Entities;

namespace Articly.Core.Domain.Interfaces;

public interface ISessionService
{
    int? GetCurrentUserId();
    Task<User> GetCurrentUser();
}
