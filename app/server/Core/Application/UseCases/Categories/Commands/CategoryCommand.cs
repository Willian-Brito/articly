using Articly.Core.Application.DTOs;
using MediatR;

namespace Articly.Core.Application.Categories.Commands;

public abstract class CategoryCommand : IRequest<CategoryDTO>
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}