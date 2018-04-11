using Reviso.TimeTracker.Infrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Reviso.TimeTracker.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TimeTrackerDbContext Dbcontext;
        public UnitOfWork(TimeTrackerDbContext dbcontext)
        {
            Dbcontext = dbcontext;
        }
       
        public async Task Commit()
        {
            await Dbcontext.SaveChangesAsync();
        }
    }
}
