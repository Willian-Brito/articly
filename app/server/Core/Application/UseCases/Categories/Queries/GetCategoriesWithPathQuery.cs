using Articly.Core.Application.Base;
using Articly.Core.Application.DTOs;
using Articly.Core.Application.Interfaces;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Categories.Queries;

public class GetCategoriesWithPathQuery : IRequest<PagedResult<CategoryWithPathDTO>>
{
    #region Query Properties
    public int PageNumber { get; set; }
    public int PageLimit { get; set; }
    #endregion

    #region Constructor
    public GetCategoriesWithPathQuery(int pageNumber, int pageLimit)
    {
        PageNumber = pageNumber;
        PageLimit = pageLimit;
    }
    #endregion

    #region Handler
    public class GetCategoriesWithPathQueryHandler : IRequestHandler<GetCategoriesWithPathQuery, PagedResult<CategoryWithPathDTO>>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        #endregion

        #region Constructor
        public GetCategoriesWithPathQueryHandler(IMapper mapper, ICategoryService categoryService)
        {
            this.mapper = mapper;
            this.categoryService = categoryService;     
        }
        #endregion

        #region Handle
        public async Task<PagedResult<CategoryWithPathDTO>> Handle(GetCategoriesWithPathQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryService.GetPaged(request.PageNumber, request.PageLimit);
            var categoriesWithPath = mapper.Map<IEnumerable<CategoryWithPathDTO>>(categories);

            foreach (var item in categoriesWithPath)
            {
                item.Path = await categoryService.GetPath(item.ID);
            }

            return new PagedResult<CategoryWithPathDTO>
            (
                items: categoriesWithPath.OrderBy(c => c.ID),
                pageNumber: request.PageNumber,
                pageLimit: request.PageLimit,
                totalCount: categoriesWithPath.Count()
            );
        }
        #endregion
    }
    #endregion
}