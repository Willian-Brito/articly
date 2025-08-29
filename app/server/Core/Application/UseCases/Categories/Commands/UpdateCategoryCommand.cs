using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Categories.Commands;

public sealed class UpdateCategoryCommand : CategoryCommand
{
    #region Command Properties
    public int ID { get; set; }
    #endregion

    #region Handler
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly ICategoryRepository categoryRepository;
        #endregion

        #region Constructor
        public UpdateCategoryCommandHandler(
            IMapper mapper,
            IUnitOfWork unityOfWork, 
            ICategoryRepository categoryRepository
        )
        {
            this.mapper = mapper;
            this.unityOfWork = unityOfWork;        
            this.categoryRepository = categoryRepository;
        }
        #endregion

        #region Handle

        public async Task<CategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.Get(request.ID);

            if (category is null) throw new NotFoundException("Categoria não existe!");
                            
            category.Update(request.Name, request.ParentId);
            await categoryRepository.Update(category);
            var dto = mapper.Map<CategoryDTO>(category);

            await unityOfWork.Commit();
            return dto;
        }
        #endregion
    }
    #endregion
}