using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Interfaces;
using Articly.Infrastructure.Security.Sessions;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Articly.Infrastructure.Security.Sessions;

public class SessionService : ISessionService
{
    #region Properties
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    #endregion

    #region Constructors
    public SessionService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IMapper mapper)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.userRepository = userRepository;
        this.mapper = mapper;
    }
    #endregion

    #region Methods

    public async Task<User> GetCurrentUser()
    {
        var userId = httpContextAccessor.HttpContext.GetCurrentUserId();
        var users = await userRepository.Get(userId.Value);
        return mapper.Map<User>(users);
    }

    public int? GetCurrentUserId()
    {
        return httpContextAccessor.HttpContext.GetCurrentUserId();
    }
    #endregion
}
