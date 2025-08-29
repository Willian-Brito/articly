using Articly.Core.Domain.Model;

namespace Articly.Core.Domain.Interfaces;

public interface IStatRepository
{
    Task<Stats> GetLast();
    Task<List<Stats>> GetAll();
    Task<Stats> Create(Stats entity);
}
