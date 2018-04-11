using System.Threading.Tasks;

namespace Reviso.TimeTracker.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
