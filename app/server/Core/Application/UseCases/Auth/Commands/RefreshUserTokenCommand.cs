using Articly.Core.Application.Auth.Interfaces;
using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Account;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using MediatR;

namespace Articly.Core.Application.Auth.Commands;

public sealed class RefreshUserTokenCommand : IRequest<UserTokenDTO>
{
    #region Command Properties
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    #endregion

    #region Handler
    public class RefreshUserTokenCommandHandler : IRequestHandler<RefreshUserTokenCommand, UserTokenDTO>
    {
        #region Properties
        private readonly IUnitOfWork unityOfWork;
        private readonly ITokenService tokenService;
        private readonly IUserRepository userRepository;
        private readonly IUserTokenRepository userTokenRepository;
        #endregion

        #region Constructor
        public RefreshUserTokenCommandHandler(
            IUnitOfWork unityOfWork,
            ITokenService tokenService,
            IUserRepository userRepository,
            IUserTokenRepository userTokenRepository
        )
        {
            this.unityOfWork = unityOfWork;
            this.tokenService = tokenService;
            this.userRepository = userRepository;
            this.userTokenRepository = userTokenRepository;
        }
        #endregion

        #region Handle
        public async Task<UserTokenDTO> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
        {
            var userToken = await userTokenRepository.GetByToken(request.Token);
            var isTokenExpired = userToken != null && DateTime.Now > userToken.AccessTokenExpiration;
            var isRefreshTokenExpired = userToken != null && DateTime.Now > userToken.RefreshTokenExpiration;

            if(!isTokenExpired)
                throw new BadRequestException("Token não expirado!");

            if(userToken.RefreshToken != request.RefreshToken)
                throw new UnauthorizedException("RefreshToken inválido!");

            if(isRefreshTokenExpired)
                throw new UnauthorizedException("RefreshToken expirado, é necessário logar novamente!");

            var claimsPrincipal = tokenService.GetClaimsFromExpiredToken(request.Token);

            var user = await userRepository.Get(userToken.UserId);
            var dto = await tokenService.Generate(user, claimsPrincipal.Claims);

            await unityOfWork.Commit();

            return dto;
        }
        #endregion
    }
    #endregion
}