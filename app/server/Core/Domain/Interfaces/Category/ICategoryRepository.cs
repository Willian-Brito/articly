using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Model;

namespace Articly.Core.Domain.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category, Categories>
{
    Task<Category> GetByName(string name);
    Task<IEnumerable<Category>> GetSubcategories(int id);
    Task<Category> GetParent(int? parentId);
    Task<int[]> GetCategoryIDsWithChildren(int id);
}
