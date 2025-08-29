using System.Security.Claims;
using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Entities;

namespace Articly.Core.Application.Auth.Interfaces;

public interface ITokenService
{
    Task<UserTokenDTO> Generate(User user, IEnumerable<Claim>? claims = null);
    ClaimsPrincipal GetClaimsFromExpiredToken(string token);
    Task<bool> IsTokenExpired(string token);
    bool IsValidToken(string token);    
}