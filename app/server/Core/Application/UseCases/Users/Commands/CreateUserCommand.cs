using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Account;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Users.Commands;

public sealed class CreateUserCommand : UserCommand
{
    #region Handler
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IRoleService roleService;
        private readonly IUnitOfWork unityOfWork;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly ISessionService sessionService;
        #endregion

        #region Constructor
        public CreateUserCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork,
            IRoleService roleService,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ISessionService sessionService            
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;
            this.roleService = roleService;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.sessionService = sessionService;
        }
        #endregion

        #region Handle
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await Validate(request);
            
            var roles = roleService.Convert(request.Roles);
            
            var user = User.Create
            (
                request.Name, 
                request.Password, 
                request.ConfirmPassword, 
                request.Email, 
                roles,
                passwordHasher
            );

            var model = await userRepository.Insert(user);
            var dto = mapper.Map<UserDTO>(user);
            await unityOfWork.Commit();            
            dto.ID = model.ID;

            return dto;
        }

        private async Task Validate(CreateUserCommand request)
        {
            var currentUser = await sessionService.GetCurrentUser();
            if(!currentUser.IsAdmin())
                throw new ForbiddenException("Sem permissão para acessar este recurso!");

            var hasEmail = await userRepository.GetByEmail(request.Email) != null;
            if (hasEmail)
                throw new BadRequestException("O e-mail de usuário já existe");
        }
        #endregion
    }
    #endregion
}