using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Articles.Queries;

public class GetArticleByNameQuery : IRequest<ArticleDTO>
{
    #region Query Properties
    public string Name { get; set; }
    #endregion

    #region Handler
    public class GetArticleByNameQueryHandler : IRequestHandler<GetArticleByNameQuery, ArticleDTO>
    {
        #region Properties
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public GetArticleByNameQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<ArticleDTO> Handle(GetArticleByNameQuery request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.GetByName(request.Name);
            var dto = mapper.Map<ArticleDTO>(article);
            return dto;
        }
        #endregion
    }
    #endregion
}