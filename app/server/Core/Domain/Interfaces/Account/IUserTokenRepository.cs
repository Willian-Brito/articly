using Articly.Core.Domain.Base;
using Articly.Core.Domain.Model;

namespace Articly.Core.Domain.Account;

public interface IUserTokenRepository : IBaseRepository<UserToken, UserTokens>
{
    Task<UserToken> GetByUser(int userId);
    Task<UserToken> GetByToken(string token);
    Task DeleteAllTokensByUser(int userId);
}
