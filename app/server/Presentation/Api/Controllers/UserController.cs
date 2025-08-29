using Articly.Core.Application.Users.Commands;
using Articly.Core.Application.Users.Queries;
using Articly.Core.Domain.Enums;
using Articly.Core.Domain.Exceptions;
using Articly.Presentation.Api.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Articly.Presentation.Api.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = nameof(Role.Administrator))]
[EnableRateLimiting("Api")]
[ApiController]
public class UserController : CustomController
{
    #region Properties
    private readonly IMediator mediator;
    #endregion

    #region Constructor
    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    #endregion

    #region Actions

    #region Commands

    #region Create
    [HttpPost]
    public async Task<ActionResult> Create(CreateUserCommand command)
    {
        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateUserCommand command)
    {
        if(id != command.ID)
            throw new BadRequestException("O ID do parâmetro da URL não corresponde ao ID do usuário do corpo da requisição");

        var dto = await mediator.Send(command);
        var response = BaseResponseAPI.Create(dto);

        return CustomResponse(response);
    }
    #endregion

    #region Delete
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand{ ID = id };
        await mediator.Send(command);

        var response = BaseResponseAPI.Create("Usuário removido com sucesso!");
        return CustomResponse(response); 
    }
    #endregion

    #endregion

    #region Queries

    #region GetPaged
    [HttpGet("paged")]    
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageLimit = 10)
    {
        var query = new GetPagedUsersQuery(pageNumber, pageLimit);
        var paged = await mediator.Send(query);

        if (paged == null)
            throw new NotFoundException("Não foi possível encontrar os usuários");         

        var response = BaseResponseAPI.Create(paged);
        return CustomResponse(response);
    }
    #endregion

    #region GetAll
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetAllUsersQuery();
        var dtos  = await mediator.Send(query);

        if (dtos is null)
            throw new NotFoundException("Não foi possível encontrar os usuários"); 

        var response = BaseResponseAPI.Create(dtos);
        return CustomResponse(response);
    }
    #endregion

    #region GetById
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery { ID = id };
        var dto = await mediator.Send(query);

        if(dto is null)
            throw new NotFoundException("Usuário não existe!");

        var response = BaseResponseAPI.Create(dto);
        return CustomResponse(response);
    }
    #endregion

    #region GetAllRoles
    [HttpGet("roles")]    
    public async Task<ActionResult> GetAllRoles()
    {
        var query = new GetAllRolesQuery();
        var dtos  = await mediator.Send(query);

        if (dtos is null)
            throw new NotFoundException("Não foi possível encontrar os perfis"); 

        var response = BaseResponseAPI.Create(dtos);
        return CustomResponse(response);
    }
    #endregion

    #endregion

    #endregion
}