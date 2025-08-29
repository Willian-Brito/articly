using Articly.Core.Application.Base;
using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Entities;
using Model = Articly.Core.Domain.Model;

namespace Articly.Core.Application.Interfaces;

public interface ICategoryService : IBaseService<Category, Model.Categories>
{
    Task<CategoryDTO> GetByName(string name);
    Task<string> GetPath(int id);
    Task<List<CategoryDTO>> GetTree(int id);
}
