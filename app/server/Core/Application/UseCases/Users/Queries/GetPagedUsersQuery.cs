using Articly.Core.Application.Base;
using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Users.Queries;

public class GetPagedUsersQuery : IRequest<PagedResult<UserDTO>>
{
    #region Query Properties
    public int PageNumber { get; set; }
    public int PageLimit { get; set; }
    #endregion

    #region Constructor
    public GetPagedUsersQuery(int pageNumber, int pageLimit)
    {
        PageNumber = pageNumber;
        PageLimit = pageLimit;
    }
    #endregion

    #region Handler
    public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, PagedResult<UserDTO>>
    {
        #region Properties
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        #endregion

        #region Cosntructor
        public GetPagedUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<PagedResult<UserDTO>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await userRepository.Count();
            var users = await userRepository.GetPaged(request.PageNumber, request.PageLimit);
            var dtos = mapper.Map<IEnumerable<UserDTO>>(users);

            return new PagedResult<UserDTO>
            (
                items: dtos.OrderBy(o => o.ID),
                pageNumber: request.PageNumber,
                pageLimit: request.PageLimit,
                totalCount: totalCount
            );
        }
        #endregion
    }
    #endregion
}