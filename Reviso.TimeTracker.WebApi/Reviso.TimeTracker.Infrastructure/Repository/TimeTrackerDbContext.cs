using Reviso.TimeTracker.Domain.Entities;
using System.Data.Entity;

namespace Reviso.TimeTracker.Infrastructure.Repository
{
    public class TimeTrackerDbContext: DbContext
    {
        public virtual IDbSet<TimeEntry> TimeEntries { get; set; }
        public TimeTrackerDbContext():base("Db:TimeTracker")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
