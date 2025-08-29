using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Exceptions;
using Articly.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Articly.Core.Application.Categories.Commands;

public sealed class CreateCategoryCommand : CategoryCommand
{
    #region Handler
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        #region Properties
        private readonly IMapper mapper;
        private readonly IUnitOfWork unityOfWork;
        private readonly ICategoryRepository categoryRepository;
        #endregion

        #region Constructor
        public CreateCategoryCommandHandler(
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

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category(request.Name, request.ParentId);

            if (newCategory is null) throw new BadRequestException("Erro ao criar categoria!");                
                            
            var model = await categoryRepository.Insert(newCategory);
            var dto = mapper.Map<CategoryDTO>(model);
            await unityOfWork.Commit();
            dto.ID = model.ID;
            
            return dto;
        }
        #endregion
    }
    #endregion
}