using Articly.Core.Domain.Base;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Model;

namespace Articly.Core.Domain.Interfaces;

public interface IArticleRepository : IBaseRepository<Article, Articles>
{
    Task<Article> GetByName(string name);
    Task<IEnumerable<Article>> GetByCategory(int categoryId);
    Task<IEnumerable<Article>> GetByUser(int userId);
    Task<IEnumerable<Articles>> GetPagedByCategories(int[] categoryIDs, int pageNumber, int pageLimit);
}
