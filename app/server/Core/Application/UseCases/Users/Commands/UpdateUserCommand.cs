using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Account;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Users.Commands;

public sealed class UpdateUserCommand : UserCommand
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IRoleService roleService;
        private readonly ISessionService sessionService;
        private readonly IUnitOfWork unityOfWork;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserRepository userRepository;
        #endregion

        #region Constructor
        public UpdateUserCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork, 
            IRoleService roleService,
            IUserRepository userRepository,
            ISessionService sessionService,
            IPasswordHasher passwordHasher
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;        
            this.roleService = roleService;        
            this.userRepository = userRepository;
            this.sessionService = sessionService;
            this.passwordHasher = passwordHasher;
        }
        #endregion

        #region Handle

        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.ID);
            
            await Validate(user);
            
            var roles = roleService.Convert(request.Roles);

            user.Update
            (
                request.Name, 
                request.Password,
                request.ConfirmPassword,
                request.Email,
                roles,
                passwordHasher
            );

            await userRepository.Update(user);
            var dto = mapper.Map<UserDTO>(user);

            await unityOfWork.Commit();
            return dto;
        }

        private async Task Validate(User user)
        {
            var currentUser = await sessionService.GetCurrentUser();

            if(!currentUser.IsAdmin())
                throw new ForbiddenException("Sem permissão para acessar este recurso!");

            if (user is null)
                throw new NotFoundException("Usuário não existe!");
        }
        #endregion
    }
    #endregion
}