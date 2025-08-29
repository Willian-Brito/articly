using Articly.Core.Application.Base;
using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Categories.Queries;

public class GetPagedCategoriesQuery : IRequest<PagedResult<CategoryDTO>>
{
    #region Query Properties
    public int PageNumber { get; set; }
    public int PageLimit { get; set; }
    #endregion

    #region Constructor
    public GetPagedCategoriesQuery(int pageNumber, int pageLimit)
    {
        PageNumber = pageNumber;
        PageLimit = pageLimit;
    }
    #endregion

    #region Handler
    public class GetPagedCategoriesQueryHandler  
        : IRequestHandler<GetPagedCategoriesQuery, PagedResult<CategoryDTO>>
    {
        #region Properties        PagedResult<
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetPagedCategoriesQueryHandler(            
            ICategoryRepository categoryRepository,
            IMapper mapper
        )
        {            
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<PagedResult<CategoryDTO>> Handle(
            GetPagedCategoriesQuery request, 
            CancellationToken cancellationToken
        )
        {
            var totalCount = await categoryRepository.Count();
            var items = await categoryRepository.GetPaged(request.PageNumber, request.PageLimit);
            var categories = mapper.Map<IEnumerable<CategoryDTO>>(items);

            return new PagedResult<CategoryDTO>
            (
                items: categories.OrderBy(o => o.ID),
                pageNumber: request.PageNumber,
                pageLimit: request.PageLimit,
                totalCount: totalCount
            );
        }
        #endregion
    }
    #endregion
}