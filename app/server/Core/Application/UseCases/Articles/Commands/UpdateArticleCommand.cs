using Articly.Core.Application.DTOs;
using Articly.Core.Application.Interfaces;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Categories.Commands;

public sealed class UpdateArticleCommand : ArticleCommand
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public UpdateArticleCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork, 
            IHtmlSanitizer htmlSanitizer,
            IArticleRepository articleRepository
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;        
            this.htmlSanitizer = htmlSanitizer;        
            this.articleRepository = articleRepository;
        }
        #endregion

        #region Handle

        public async Task<ArticleDTO> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await articleRepository.Get(request.ID);

            if (article is null) throw new NotFoundException("Artigo não existe!");

            var sanitizedBytes = htmlSanitizer.Sanitize(request.Content);
                            
            article.Update
            (
                request.Name, 
                (int)request.CategoryId, 
                (int)request.UserId, 
                request.Description, 
                sanitizedBytes,
                request.ImageUrl
            );
            
            await articleRepository.Update(article);
            var dto = mapper.Map<ArticleDTO>(article);

            await unityOfWork.Commit();
            return dto;
        }
        #endregion
    }
    #endregion
}