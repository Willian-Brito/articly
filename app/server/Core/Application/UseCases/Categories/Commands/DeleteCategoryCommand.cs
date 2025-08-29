using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using MediatR;

namespace Articly.Core.Application.Categories.Commands;

public sealed class DeleteCategoryCommand : IRequest<Category>
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Category>
    {
        #region Properties
        private readonly IUnitOfWork unityOfWork;
        private readonly ICategoryRepository categoryRepository;
        private readonly IArticleRepository articleRepository;
        #endregion

        #region Constructor
        public DeleteCategoryCommandHandler(
            IUnitOfWork unityOfWork, 
            ICategoryRepository categoryRepository,
            IArticleRepository articleRepository
        )
        {
            this.unityOfWork = unityOfWork;        
            this.categoryRepository = categoryRepository;
            this.articleRepository = articleRepository;
        }
        #endregion

        #region Handle

        public async Task<Category> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.Delete(request.ID);     

            await Validate(category);
            await unityOfWork.Commit();

            return category;
        }

        private async Task Validate(Category category)
        {
            if (category is null) throw new NotFoundException("Categoria não existe!");

            var subCategories = await categoryRepository.GetSubcategories(category.ID);

            if(subCategories.Count() != 0)
                throw new BadRequestException("Categoria possui subcategorias!");

            var articles = await articleRepository.GetByCategory(category.ID);

            if (articles.Count() != 0)
                throw new BadRequestException("Categoria possui artigos vinculados!");
        }
        #endregion
    }
    #endregion
}