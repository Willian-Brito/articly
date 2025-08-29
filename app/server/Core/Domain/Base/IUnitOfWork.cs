using System.Data;

namespace Articly.Core.Domain.Base;

public interface IUnitOfWork
{
    Task Commit();
    void Rollback();
}
