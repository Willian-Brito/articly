using Articly.Core.Application.Categories.Commands;
using Articly.Core.Application.Categories.Queries;
using Articly.Core.Domain.Enums;
using Articly.Core.Domain.Exceptions;
using Articly.Presentation.Api.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Articly.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("Api")]
[Authorize]
public class CategoryController : CustomController
{
    #region Properties
    private readonly IMediator mediator;
    #endregion

    #region Constructor
    public CategoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    #endregion

    #region Actions

    #region Commands

    #region Create

    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Create(CreateCategoryCommand command)
    {
        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Update
    [HttpPut("{id:int}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Update(int id, UpdateCategoryCommand command)
    {
        if(id != command.ID)
            throw new BadRequestException("O ID do parâmetro da URL não corresponde ao ID da categoria do corpo da requisição");

        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Delete
    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCategoryCommand{ ID = id };
        await mediator.Send(command);

        var response = BaseResponseAPI.Create("Categoria removida com sucesso!");
        return CustomResponse(response);
    }
    #endregion

    #endregion

    #region Queries

    #region GetPaged
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageLimit = 10)
    {
        var query = new GetPagedCategoriesQuery(pageNumber, pageLimit);
        var paged = await mediator.Send(query);

        if (paged is null)
            throw new NotFoundException("Não foi possível encontrar as categorias");

        var response = BaseResponseAPI.Create(paged);
        return CustomResponse(response);
    }
    #endregion

    #region GetAll
    [HttpGet]
    [Authorize(Roles = nameof(Role.Administrator))]
    // [AllowAnonymous]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllCategoriesQuery();
        var dtos  = await mediator.Send(query);

        if (dtos is null)
            throw new NotFoundException("Não foi possível encontrar as categorias"); 

        var response = BaseResponseAPI.Create(dtos);
        return CustomResponse(response);
    }
    #endregion

    #region GetById
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var query = new GetCategoryByIdQuery { ID = id };
        var dto = await mediator.Send(query);

        if(dto is null)
            throw new NotFoundException("Categoria não existe!");

        var response = BaseResponseAPI.Create(dto);
        return CustomResponse(response);
    }
    #endregion

    #region GetByName

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetByName(string name)
    {
        var query = new GetCategoryByNameQuery { Name = name };
        var dto  = await mediator.Send(query);

        if(dto == null)
            throw new NotFoundException("Categoria não existe!");

        var response = BaseResponseAPI.Create(dto);
        return CustomResponse(response);
    }
    #endregion

    #region GetCategoriesWithPath

    [HttpGet("path")]
    public async Task<ActionResult> GetCategoriesWithPath([FromQuery] int pageNumber = 1, [FromQuery] int pageLimit = 10)
    {
        var query = new GetCategoriesWithPathQuery(pageNumber, pageLimit);
        var dto  = await mediator.Send(query);
        var response = BaseResponseAPI.Create(dto);
        
        return CustomResponse(response);
    }
    #endregion

    #region GetCategoriesWithTree

    [HttpGet("tree")]
    public async Task<ActionResult> GetCategoriesWithTree()
    {        
        var query = new GetCategoriesWithTreeQuery();
        var dto  = await mediator.Send(query);
        var response = BaseResponseAPI.Create(dto);
        
        return CustomResponse(response);
    }
    #endregion

    #endregion

    #endregion
}