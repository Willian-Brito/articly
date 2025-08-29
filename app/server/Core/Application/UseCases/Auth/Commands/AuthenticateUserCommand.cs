using Articly.Core.Application.Auth.Interfaces;
using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Account;
using Articly.Core.Domain.Base;
using MediatR;

namespace Articly.Core.Application.Auth.Commands;

public sealed class AuthenticateUserCommand : IRequest<UserTokenDTO>
{
    #region Properties Command
    public string? Email { get; set; }
    public string? Password { get; set; }
    #endregion

    #region Handler
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserTokenDTO>
    {
        #region Properties
        private readonly IAuthenticateService authenticateService;
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Constructor
        public AuthenticateUserCommandHandler(
            IAuthenticateService authenticateService, 
            ITokenService tokenService,
            IUnitOfWork unitOfWork
        )
        {
            this.authenticateService = authenticateService;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Handle
        public async Task<UserTokenDTO> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await authenticateService.Authenticate(request.Email, request.Password);
            var dto = await tokenService.Generate(user);

            await unitOfWork.Commit();

            return dto;
        }
        #endregion
    }
    #endregion    
}