using Articly.Core.Application.DTOs;
using Articly.Core.Application.Interfaces;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Categories.Commands;

public sealed class CreateArticleCommand : ArticleCommand
{
    #region Handler
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public CreateArticleCommandHandler(
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

        public async Task<ArticleDTO> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {         
            var sanitizedBytes = htmlSanitizer.Sanitize(request.Content);

            var newArticle = new Article
            (
                request.Name, 
                request.CategoryId ?? 0, 
                request.UserId ?? 0, 
                request.Description, 
                // sanitizedBytes,
                request.Content,                 
                request.ImageUrl
            );

            if (newArticle is null) throw new BadRequestException("Erro ao criar Artigo!");
                            
            var model = await articleRepository.Insert(newArticle);
            var dto = mapper.Map<ArticleDTO>(model);
            await unityOfWork.Commit();

            dto.ID = model.ID;

            return dto;
        }
        #endregion
    }
    #endregion
}