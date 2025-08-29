using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Users.Queries;

public class GetUserByIdQuery : IRequest<UserDTO>
{
    #region Query Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        #region Properties
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.ID);
            var dto = mapper.Map<UserDTO>(user);
            return dto;
        }
        #endregion
    }
    #endregion
}